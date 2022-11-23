import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';

import { IGoogleTagManger } from '@core/models/google-tag-manager.model';

@Injectable()
export class GoogleTagManagerService {

  private readonly isBrowser: boolean;

  constructor(
    @Inject(PLATFORM_ID) private readonly platformId: any,
    @Inject(DOCUMENT) private readonly document: Document
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  pushTag(tag: any) {
    if (this.isBrowser) {
      const googleTagManger = this.document.defaultView as any as IGoogleTagManger;
      if (googleTagManger.dataLayer) {
        googleTagManger.dataLayer.push(tag);
      }
    }
  }
}
