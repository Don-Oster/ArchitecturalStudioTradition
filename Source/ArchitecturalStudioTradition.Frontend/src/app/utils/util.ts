export const env = {
  isWebKit: typeof document !== 'undefined' && 'WebKitAppearance' in document.documentElement.style,
  supportTouch: typeof window !== 'undefined' && ('ontouchstart' in window || window['DocumentTouch'] && document instanceof window['DocumentTouch']),
  supportsIePointer: typeof navigator !== 'undefined' && navigator['msMaxTouchPoints'],
  isChrome: typeof navigator !== 'undefined' && /Chrome/i.test(navigator && navigator.userAgent),
};

export function toInteger(value: any): number {
  return parseInt(`${value}`, 10) || 0;
}

export function toString(value: any): string {
  return (value !== undefined && value !== null) ? `${value}` : '';
}

export function getValueInRange(value: number, max: number, min: number = 0): number {
  return Math.max(Math.min(value, max), min);
}

export function isString(value: any): boolean {
  return typeof value === 'string';
}

export function isNumber(value: any): boolean {
  return !isNaN(toInteger(value));
}

export function isInteger(value: any): boolean {
  return typeof value === 'number' && isFinite(value) && Math.floor(value) === value;
}

export function isObjectArray<T>(value: T[]): boolean {
  const parser: Function = Object.prototype.toString;
  if (parser.call(value) === '[object Array]') {
    if (parser.call(value[0]) === '[object Object]') {
      return true;
    }
  }
  return false;
}

export function isNullOrUndefined(value: any): boolean {
  return value === undefined || value === null;
}

export function isUndefined(value: Object): boolean {
  return ('undefined' === typeof value);
}

export function isDefined(value: any): boolean {
  return value !== undefined && value !== null;
}

export function padNumber(value: number): string {
  if (isNumber(value)) {
    return `0${value}`.slice(-2);
  } else {
    return '';
  }
}

/* tslint:disable */
export function applyMixins(derivedClass: any, baseClass: any[]): void {
  baseClass.forEach(baseClass => {
    Object.getOwnPropertyNames(baseClass.prototype).forEach(name => {
      if (!derivedClass.prototype.hasOwnProperty(name) || baseClass.isFormBase) {
        derivedClass.prototype[name] = baseClass.prototype[name];
      }
    });
  });
}

/* tslint:disable */
export function ComponentMixins(baseClass: Function[]): ClassDecorator {
  return function (derivedClass: Function) {
    applyMixins(derivedClass, baseClass);
  };
}

export function getValue(nameSpace: string, obj: any): any {
  // eslint-disable-next-line
  let value: any = obj;
  const splits: string[] = nameSpace.replace(/\[/g, '.').replace(/\]/g, '').split('.');

  for (let i: number = 0; i < splits.length && !isUndefined(value); i++) {
    value = value[splits[i]];
  }
  return value;
}

export function setValue(nameSpace: string, value: any, obj: any): any {
  const keys: string[] = nameSpace.replace(/\[/g, '.').replace(/\]/g, '').split('.');
  // eslint-disable-next-line
  const start: any = obj || {};
  // eslint-disable-next-line
  let fromObj: any = start;
  let i: number;
  const length: number = keys.length;
  let key: string;

  for (i = 0; i < length; i++) {
    key = keys[i];

    if (i + 1 === length) {
      fromObj[key] = value === undefined ? {} : value;
    } else if (isNullOrUndefined(fromObj[key])) {
      fromObj[key] = {};
    }

    fromObj = fromObj[key];
  }

  return start;
}

export function queryParams(data: any): string {
  const array: string[] = [];
  const keys: string[] = Object.keys(data);
  for (const key of keys) {
    array.push(encodeURIComponent(key) + '=' + encodeURIComponent('' + data[key]));
  }
  return array.join('&');
}

export function regExpEscape(text: string): string {
  return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, '\\$&');
}

const htmlEscapes = {
  '<': '&lt;',
  '>': '&gt;',
  '"': '&quot;',
  '\'': '&#x27;'
},
  htmlEscaper = /[<>"']/g;

export function escapeHtml(text: string): string {
  return ('' + text).replace(htmlEscaper, function (match: string): string {
    return htmlEscapes[match];
  });
}

export function unescapeHtml(text: string): string {
  var el: HTMLDivElement = document.createElement('div');
  el.innerHTML = text;

  return el.childNodes.length === 0 ? '' : el.childNodes[0].nodeValue;
}
