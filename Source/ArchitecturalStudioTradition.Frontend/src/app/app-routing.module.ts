import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { NotFoundPageComponent } from '@core/pages/not-found-page/not-found-page.component';
import { TermsConditionsPageComponent } from '@app/core/pages/terms-conditions/terms-conditions.component';

import { Page } from '@shared/page.enum';

import { routes as AccountRoutes } from '@app/modules/account/account.routes';
import { routes as InteriorsRoutes } from '@app/modules/interiors/interiors.routes';
import { routes as ArchitectureRoutes } from '@app/modules/architecture/architecture.routes';
import { routes as BlogRoutes } from '@app/modules/blog/blog.routes'

import { HomeComponent } from './modules/home/home.component';
import { AboutComponent } from './modules/about/about.component';

const APP_ROUTES: Routes = [
  {
    path: `${Page.Home}`,
    component: HomeComponent
  },
  {
    path: `${Page.Interiors}`,
    children: [
      ...InteriorsRoutes,
    ]
  },
  {
    path: `${Page.Account}`,
    children: [
      ...AccountRoutes,
    ]
  },
  {
    path: `${Page.Architecture}`,
    children: [
      ...ArchitectureRoutes,
    ]
  },
  {
    path: `${Page.Blog}`,
    children: [
      ...BlogRoutes,
    ]
  },
  //{
  //  path: `${Page.Team}`,
  //  component: TeamComponent
  //},
  {
    path: `${Page.About}`,
    component: AboutComponent
  },
  {
    path: `${Page.NotFound}`,
    component: NotFoundPageComponent
  },
  {
    path: `${Page.TermsConditions}`,
    component: TermsConditionsPageComponent
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      APP_ROUTES,
      {
        preloadingStrategy: PreloadAllModules
      }
    )
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
