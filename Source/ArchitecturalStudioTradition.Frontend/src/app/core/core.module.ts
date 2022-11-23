import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

/** Modules */
import { SharedModule } from '@shared/shared.module';

/** Components */
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';
import { TermsConditionsPageComponent } from './pages/terms-conditions/terms-conditions.component';

const Modules = [
];

const Components = [
  NotFoundPageComponent,
  TermsConditionsPageComponent,
];

@NgModule({
  declarations: [
    ...Components

  ],
  imports: [
    ...Modules,
    SharedModule,
    CommonModule,
    RouterModule
  ],
  exports: [
    ...Modules,
    ...Components
  ]
})
export class CoreModule { }
