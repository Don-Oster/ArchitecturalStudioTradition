import { TransferHttpCacheModule } from '@nguniversal/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { APP_BASE } from '../app/app-module.base';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'arch-tradition-ssr' }),
    TransferHttpCacheModule,
    ...APP_BASE.imports,
  ],
  declarations: [
    ...APP_BASE.declarations,
  ],
  bootstrap: [
    ...APP_BASE.bootstrap,
  ],
  providers: [
    ...APP_BASE.providers,
  ]
})
export class SsrModule { }
