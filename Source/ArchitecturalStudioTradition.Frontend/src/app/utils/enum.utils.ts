import {
  isNumber,
  isString
} from '../utils/util';

export function getEnumValues(en: any): string[] {
  return Object
    .keys(en)
    .map(k => en[k])
    .filter(v => !isNumber(v)) as string[];
}

export function checkEnumValue(en: any, value: string, dflt: string | number = undefined): string {
  return en[en[value]] ||
    (isString(dflt) ? en[en[dflt]] : en[dflt]);
}
