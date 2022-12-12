import { Directive, ElementRef,HostListener } from '@angular/core';

@Directive({
  selector: '[focusInvalidInput]'
})
export class FormDirective {
  constructor(private el: ElementRef) { }

  @HostListener('submit')
  onFormSubmit() {
    this.el.nativeElement
      .querySelector('input.ng-invalid')
      ?.focus();
  }
}
