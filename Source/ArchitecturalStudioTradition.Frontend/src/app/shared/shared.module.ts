import { NgModule } from '@angular/core';
import { CommonModule, DatePipe, LowerCasePipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BaseLinkPipe } from './pipes/base-link/base-link.pipe';
import { SessionStorageService } from './services/session-storage.service';
import { LinksService } from './services/links/links.service';
import { BaseMetaTagsService } from './services/meta-tags/base-meta-tags.service';
import { MetaTagsService } from './services/meta-tags/meta-tags.service';
import { PageMetaService } from './services/meta-tags/page-meta.service';
import { GoogleTagManagerService } from './services/google-tag-manager/google-tag-manager.service';
import { GoogleAnalyticsService } from './services/google-analytics/google-analytics.service';

const PIPES = [
  BaseLinkPipe
];

const MODULES = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule
];

const COMPONENTS = [
];

const PROVIDERS = [
  SessionStorageService,
  LinksService,
  BaseMetaTagsService,
  MetaTagsService,
  PageMetaService,
  GoogleTagManagerService,
  GoogleAnalyticsService
];

@NgModule({
  imports: [
    RouterModule,
    ...MODULES,
  ],
  declarations: [
    ...COMPONENTS,
    ...PIPES,
  ],
  exports: [
    ...MODULES,
    ...COMPONENTS,
    ...PIPES,
  ],
  providers: [
    ...PIPES,
    ...PROVIDERS,
    DatePipe,
    LowerCasePipe
  ]
})
export class SharedModule { }
