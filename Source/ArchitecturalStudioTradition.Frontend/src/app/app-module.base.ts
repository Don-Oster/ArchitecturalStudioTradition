import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ServiceWorkerModule } from '@angular/service-worker';

//import { StoreModule } from '@ngrx/store';
//import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { PROVIDERS as ContractProviders } from '@archtradition-contract';

import { SwiperModule } from 'swiper/angular';

import {
  GoogleLoginProvider,
  SocialLoginModule,
  FacebookLoginProvider,
  SocialAuthServiceConfig,
} from '@abacritt/angularx-social-login';

import {
  ConnectionServiceModule, ConnectionServiceOptions,
  ConnectionServiceOptionsToken
} from 'angular-connection-service';

import { CoreModule } from '@core/core.module';
import { WebApiUrlInterceptor } from '@core/interceptors/webapi-url.interceptor';
import { API_BASE_URL_TOKEN } from '@core/interceptors/api-base-url.token';
//import { reducer } from '@store/app.reducer';
//import { featureName } from '@store/app.selectors';
import { SessionStorageService } from '@shared/services/session-storage.service';
import { LinksService } from '@shared/services/links/links.service';
import { BaseMetaTagsService } from '@shared/services/meta-tags/base-meta-tags.service';
import { MetaTagsService } from '@shared/services/meta-tags/meta-tags.service';
import { PageMetaService } from '@shared/services/meta-tags/page-meta.service';
import { GoogleTagManagerService } from '@shared/services/google-tag-manager/google-tag-manager.service';
import { GoogleAnalyticsService } from '@shared/services/google-analytics/google-analytics.service';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { environment } from '../environments/environment';

import { HomeModule } from './modules/home/home.module';
import { AboutModule } from './modules/about/about.module';
import { TranslocoRootModule } from './transloco-root.module';

const FEATURE_MODULES = [
  HomeModule,
  AboutModule
];

export const APP_BASE: NgModule = {
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    //StoreModule.forRoot(reducer),
    //StoreDevtoolsModule.instrument({
    //  maxAge: 25
    //}),
    AppRoutingModule,
    CommonModule,
    TranslocoRootModule,
    CoreModule,
    HttpClientModule,
    SocialLoginModule,
    SwiperModule,
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
