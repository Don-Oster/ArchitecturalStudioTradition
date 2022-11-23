import 'zone.js/dist/zone-node';

import { APP_BASE_HREF } from '@angular/common';
import { ngExpressEngine } from '@nguniversal/express-engine';
import * as express from 'express';
import {join} from 'path';
import { existsSync } from 'fs';

import { SsrServerModule, SSR_API_BASE_URL_TOKEN, META_BASE_URL_TOKEN } from './src/main.ssr.server';

// Configuration
const DIST_FOLDER_URI = 'dist/browser';
const STATIC_FOLDER_URI = 'dist/server/static';
const STATIC_FILES_URL = `/browser`;
const DIST_FOLDER = join(process.cwd(), DIST_FOLDER_URI);
const STATIC_FOLDER = join(process.cwd(), STATIC_FOLDER_URI);
const SSR_WEBAPI_URL = process.env.ARCHTRADITION__FRONTEND__SsrWebApiUrl; // http://archtradition-cache/Public
const SSR_BASE_URL = process.env.ARCHTRADITION__FRONTEND__BaseUrl; // https://archtradition.com
const USE_PROD_ROBOTS = (process.env.ARCHTRADITION_FRONTEND__UseProdRobots || '').trim() === 'true';
const INDEX_HTML = existsSync(join(DIST_FOLDER, 'index.original.html')) ? 'index.original.html' : 'index';

// Express server
export function app() {
  const server = express();

  // Our Universal express-engine (found @ https://github.com/angular/universal/tree/master/modules/express-engine)
  server.engine('html', ngExpressEngine({
    bootstrap: SsrServerModule,
    providers: [
      { provide: SSR_API_BASE_URL_TOKEN, useValue: SSR_WEBAPI_URL },
      { provide: META_BASE_URL_TOKEN, useValue: SSR_BASE_URL },
    ]
  }));

  server.set('view engine', 'html');
  server.set('views', DIST_FOLDER);

  // Serve static files from /dist/browser
  server.use(STATIC_FILES_URL, express.static(DIST_FOLDER, {
      maxAge: '1y'
    })
  );

  // Serve robots.txt file from /dist/server/static
  server.use('/robots.txt', (req, res) => {
    const filePath = USE_PROD_ROBOTS ? 'robots.prod.txt' : 'robots.txt';
    res.sendFile(join(STATIC_FOLDER, filePath));
  });

  // Serve favicon.ico file from /dist/browser
  server.use('/favicon.ico', (req, res) => {
      res.sendFile(join(DIST_FOLDER, 'favicon.ico'));
  });

  // All regular routes use the Universal engine
  server.get('*', (req, res) => {
    res.render(INDEX_HTML, { req, providers: [{ provide: APP_BASE_HREF, useValue: req.baseUrl }] });
  });

  return server;
}

function run() {
  const port = process.env.PORT || 4000;

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

export * from './src/main.ssr.server';
