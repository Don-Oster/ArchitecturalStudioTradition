import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';

@Injectable()
export class SessionStorageService {

  constructor(
    @Inject(PLATFORM_ID) private platformId: any
  ) { }

  setItem(key: string, value: any) {
    if (isPlatformBrowser(this.platformId)) {
      sessionStorage.setItem(key, JSON.stringify(value));
    }
  }

  getItem(key: string): any {
    if (isPlatformBrowser(this.platformId)) {
      return JSON.parse(sessionStorage.getItem(key));
    } else {
      return null;
    }
  }

  clearItem(key: string): any {
    if (isPlatformBrowser(this.platformId)) {
      sessionStorage.removeItem(key);
    }
  }

}
