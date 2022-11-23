import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeComponent } from './home.component';

const COMPONENTS = [
  HomeComponent
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
export class HomeModule { }
