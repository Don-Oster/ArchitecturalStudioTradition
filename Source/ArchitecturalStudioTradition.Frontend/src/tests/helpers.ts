import { DebugElement, Type } from '@angular/core';
import { By } from '@angular/platform-browser';


export function getComponent<T>(debugElement: DebugElement, type: Type<T>, index = 0): T {
    return debugElement.queryAll(By.directive(type))[index].injector.get(type) as T;
}
