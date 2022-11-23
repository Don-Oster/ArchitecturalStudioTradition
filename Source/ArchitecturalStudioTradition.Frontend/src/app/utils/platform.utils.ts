import { Injectable } from '@angular/core';

declare const window: any;

// Whether the current platform supports the V8 Break Iterator. The V8 check
// is necessary to detect all Blink based browsers.
const hasV8BreakIterator: any = typeof (window) !== 'undefined' ?
  (window.Intl && (window.Intl as any).v8BreakIterator) :
  (typeof (Intl) !== 'undefined' && (Intl as any).v8BreakIterator);

@Injectable()
export class Platform {
  EDGE: boolean = /(edge)/i.test(navigator.userAgent);
  TRIDENT: boolean = /(msie|trident)/i.test(navigator.userAgent);
  BLINK: boolean = !!(window.chrome || hasV8BreakIterator) && !!CSS && !this.EDGE && !this.TRIDENT;
  WEBKIT: boolean = /AppleWebKit/i.test(navigator.userAgent) && !this.BLINK && !this.EDGE && !this.TRIDENT;
  IOS: boolean = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
  FIREFOX: boolean = /(firefox|minefield)/i.test(navigator.userAgent);
  ANDROID: boolean = /android/i.test(navigator.userAgent) && !this.TRIDENT;
}
