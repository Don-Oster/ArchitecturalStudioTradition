import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { APP_BASE } from '../app/app-module.base';

@NgModule({
  imports: [
    BrowserModule,
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
export class SelfhostModule {}
