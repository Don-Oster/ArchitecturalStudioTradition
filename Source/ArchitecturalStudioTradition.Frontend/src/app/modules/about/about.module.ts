import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AboutComponent } from './about.component';

const COMPONENTS = [
  AboutComponent
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
export class AboutModule { }
