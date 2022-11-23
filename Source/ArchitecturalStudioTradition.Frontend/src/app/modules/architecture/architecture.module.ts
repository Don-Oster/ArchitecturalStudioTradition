import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ArchitectureComponent } from './architecture.component';

const COMPONENTS = [
  ArchitectureComponent
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
export class ArchitectureModule { }
