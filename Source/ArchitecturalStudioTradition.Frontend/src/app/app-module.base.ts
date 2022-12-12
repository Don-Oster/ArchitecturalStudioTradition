import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS,HttpClientModule } from '@angular/common/http';
import { isDevMode, NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';

import {
  FacebookLoginProvider,
  GoogleLoginProvider,
  SocialAuthServiceConfig,
  SocialLoginModule,
} from '@abacritt/angularx-social-login';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import {
  ConnectionServiceModule, ConnectionServiceOptions,
  ConnectionServiceOptionsToken
} from 'angular-connection-service';
import { PROVIDERS as ContractProviders } from 'archtradition-contract';

import { CoreModule } from '@core/core.module';
import { API_BASE_URL_TOKEN } from '@core/interceptors/api-base-url.token';
import { WebApiUrlInterceptor } from '@core/interceptors/webapi-url.interceptor';
import { GoogleAnalyticsService } from '@shared/services/google-analytics/google-analytics.service';
import { GoogleTagManagerService } from '@shared/services/google-tag-manager/google-tag-manager.service';
import { LinksService } from '@shared/services/links/links.service';
import { BaseMetaTagsService } from '@shared/services/meta-tags/base-meta-tags.service';
import { MetaTagsService } from '@shared/services/meta-tags/meta-tags.service';
import { PageMetaService } from '@shared/services/meta-tags/page-meta.service';
import { SessionStorageService } from '@shared/services/session-storage.service';

import { environment } from '../environments/environment';

import { AboutModule } from './modules/about/about.module';
import { HomeModule } from './modules/home/home.module';
import { AppComponent } from './app.component';
import { AppEffects } from './app.effects';
import { AppRoutingModule } from './app-routing.module';
import { metaReducers,reducers } from './reducers';

const FEATURE_MODULES = [
  HomeModule,
  AboutModule
];

export const APP_BASE: NgModule = {
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    StoreModule.forRoot(reducers, { metaReducers }),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production
    }),
    EffectsModule.forRoot([AppEffects]),
    AppRoutingModule,
    CommonModule,
    CoreModule,
    HttpClientModule,
    SocialLoginModule,
    ConnectionServiceModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      registrationStrategy: 'registerWhenStable:30000'
    }),
    ...FEATURE_MODULES
  ],
  providers: [
    { provide: API_BASE_URL_TOKEN, useValue: environment.apiBaseUrl },
    { provide: HTTP_INTERCEPTORS, useClass: WebApiUrlInterceptor, multi: true },
    {
      provide: ConnectionServiceOptionsToken,
      useValue: <ConnectionServiceOptions>{
        heartbeatUrl: environment.baseUrl + 'api/heartbeat',
      }
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('clientId')
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('clientId')
          }
        ],
      } as SocialAuthServiceConfig,
    },
    ...ContractProviders,
    SessionStorageService,
    LinksService,
    BaseMetaTagsService,
    MetaTagsService,
    PageMetaService,
    GoogleTagManagerService,
    GoogleAnalyticsService
  ],
  declarations: [
    AppComponent
  ],
  bootstrap: [
    AppComponent
  ]
};
