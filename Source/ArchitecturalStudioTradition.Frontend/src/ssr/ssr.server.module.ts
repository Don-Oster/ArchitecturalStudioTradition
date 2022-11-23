import { NgModule } from '@angular/core';
import { ServerModule, ServerTransferStateModule } from '@angular/platform-server';
import { APP_BASE } from '../app/app-module.base';

import { SsrModule } from './ssr.module';

@NgModule({
  imports: [
    SsrModule,
    ServerModule,
    ServerTransferStateModule
  ],
  bootstrap: [
    ...APP_BASE.bootstrap,
  ],
})
export class SsrServerModule {}
