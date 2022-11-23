import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { Provider } from '@angular/core';

import { API_BASE_URL, AccountsService } from './api-generated.contract';

import { EveryRequestInterceptor } from './interceptors/every-request.interceptor';

const SERVICES = [
  AccountsService
];

export const ApiBaseUrlProvider: Provider = {
  provide: API_BASE_URL,
  useValue: '/api'
};

export const PROVIDERS = [
  { provide: HTTP_INTERCEPTORS, useClass: EveryRequestInterceptor, multi: true },
  ApiBaseUrlProvider,
  ...SERVICES
];

