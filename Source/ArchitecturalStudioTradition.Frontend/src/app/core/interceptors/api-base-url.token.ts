import { InjectionToken } from '@angular/core';

export const API_BASE_URL_TOKEN: InjectionToken<string> =
  new InjectionToken<string>('ArchTradition.WebApi base url');

export const SSR_API_BASE_URL_TOKEN: InjectionToken<string> =
  new InjectionToken<string>('ArchTradition.WebApi base url for Server-Side Rendering');
