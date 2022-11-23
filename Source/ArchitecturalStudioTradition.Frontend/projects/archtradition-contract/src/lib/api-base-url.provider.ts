import { Provider } from '@angular/core';
import { API_BASE_URL } from './api-generated.contract';

export const ApiBaseUrlProvider: Provider = {
    provide: API_BASE_URL,
    useValue: '/api'
};
