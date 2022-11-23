import { enableProdMode } from '@angular/core';

import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

export { SsrServerModule } from './ssr/ssr.server.module';
export { SSR_API_BASE_URL_TOKEN } from '@core/interceptors/api-base-url.token';
export { META_BASE_URL_TOKEN } from '@shared/services/meta-tags/meta-base-url.token';
export { renderModule, renderModuleFactory } from '@angular/platform-server';
