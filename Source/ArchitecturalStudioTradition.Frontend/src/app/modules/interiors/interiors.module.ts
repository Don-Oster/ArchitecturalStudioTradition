import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InteriorsComponent } from './interiors.component';

const COMPONENTS = [
  InteriorsComponent
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
export class InteriorsModule { }
