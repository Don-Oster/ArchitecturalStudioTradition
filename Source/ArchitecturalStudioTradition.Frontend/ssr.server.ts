import { APP_BASE_HREF } from '@angular/common';

import { ngExpressEngine } from '@nguniversal/express-engine';
import * as express from 'express';
import { existsSync } from 'fs';
import { join } from 'path';

import { META_BASE_URL_TOKEN, SSR_API_BASE_URL_TOKEN, SsrServerModule } from './src/main.ssr.server';

import 'zone.js/node';

// The Express app is exported so that it can be used by serverless Functions.
export function app(): express.Express {
  const server = express();
  const distFilesUrl = 'dist/browser';
  const staticFilesUrl = 'dist/server/static';
  const distFolder = join(process.cwd(), distFilesUrl);
  const staticFolder = join(process.cwd(), staticFilesUrl);
  const indexHtml = existsSync(join(distFolder, 'index.original.html')) ? 'index.original.html' : 'index';
  const useProdRobots = (process.env.ARCHTRADITION_FRONTEND__UseProdRobots || '').trim() === 'true';
  const ssrWebApiUrl = process.env.ARCHTRADITION__FRONTEND__SsrWebApiUrl; // http://archtradition-cache/Public
  const ssrBaseUrl = process.env.ARCHTRADITION__FRONTEND__BaseUrl; // https://archtradition.com

  // Our Universal express-engine (found @ https://github.com/angular/universal/tree/main/modules/express-engine)
  server.engine('html', ngExpressEngine({
    bootstrap: SsrServerModule,
    providers: [
      { provide: SSR_API_BASE_URL_TOKEN, useValue: ssrWebApiUrl },
      { provide: META_BASE_URL_TOKEN, useValue: ssrBaseUrl },
    ]
  }));

  server.set('view engine', 'html');
  server.set('views', distFolder);

  // Serve static files from /dist/browser
  server.use(staticFilesUrl, express.static(distFolder, {
    maxAge: '1y'
  })
  );

  // Serve robots.txt file from /dist/server/static
  server.use('/robots.txt', (req, res) => {
    const filePath = useProdRobots ? 'robots.prod.txt' : 'robots.txt';
    res.sendFile(join(staticFolder, filePath));
  });

  // Serve favicon.ico file from /dist/browser
  server.use('/favicon.ico', (req, res) => {
    res.sendFile(join(distFolder, 'favicon.ico'));
  });

  // All regular routes use the Universal engine
  server.get('*', (req, res) => {
    res.render(indexHtml, { req, providers: [{ provide: APP_BASE_HREF, useValue: req.baseUrl }] });
  });

  return server;
}

function run(): void {
  const port = process.env['PORT'] || 4000;

  // Start up the Node server
  const server = app();
  server.listen(port, () => {
    console.log(`Node Express server listening on http://localhost:${port}`);
  });
}

// Webpack will replace 'require' with '__webpack_require__'
// '__non_webpack_require__' is a proxy to Node 'require'
// The below code is to ensure that the server is run only when not requiring the bundle.
declare const __non_webpack_require__: NodeRequire;
const mainModule = __non_webpack_require__.main;
const moduleFilename = mainModule && mainModule.filename || '';
if (moduleFilename === __filename || moduleFilename.includes('iisnode')) {
  run();
}

export * from './src/main.server';
