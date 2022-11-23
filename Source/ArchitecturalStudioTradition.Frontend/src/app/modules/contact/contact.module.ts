import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactComponent } from './contact.component';

const COMPONENTS = [
  ContactComponent
]

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    ...COMPONENTS
  ],
  exports: [
    ...COMPONENTS
  ]
})
export class ContactModule { }
