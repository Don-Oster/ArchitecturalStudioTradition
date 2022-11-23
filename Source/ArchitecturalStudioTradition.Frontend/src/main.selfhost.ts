import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from './environments/environment';
import { SelfhostModule } from './selfhost/selfhost.module';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(SelfhostModule, { preserveWhitespaces: true })
  .catch(err => console.error(err));
