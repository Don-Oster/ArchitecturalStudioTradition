import { isPlatformServer } from '@angular/common';
import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Inject, Injectable, Optional, PLATFORM_ID } from '@angular/core';

import { Observable } from 'rxjs';

import { API_BASE_URL_TOKEN, SSR_API_BASE_URL_TOKEN } from './api-base-url.token';

const DEFAULT_BASE_API_URL = '/api/';

@Injectable()
export class WebApiUrlInterceptor {

    constructor(
        @Inject(API_BASE_URL_TOKEN) @Optional() private readonly baseUrl: string,
        @Inject(SSR_API_BASE_URL_TOKEN) @Optional() private readonly ssrBaseUrl: string,
        @Inject(PLATFORM_ID) private platformId: any
    ) { }

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        request = request.clone({
            url: this.processUrl(request.url)
        });

        return next.handle(request);
    }

    private processUrl(url: string): string {
        const apiBaseUrl = isPlatformServer(this.platformId) ? this.ssrBaseUrl : this.baseUrl;

        if (apiBaseUrl) {
            const baseUrl = this.prepareUrl(apiBaseUrl);
            url = url.replace(DEFAULT_BASE_API_URL, baseUrl);
        }

        return url;
    }

    private prepareUrl(url: string): string {
        url = url.trim();

        if (!url.startsWith('/') && !url.startsWith('http')) {
            url = `/${url}`;
        }

        if (!url.endsWith('/')) {
            url = `${url}/`;
        }

        return url;
    }

}
