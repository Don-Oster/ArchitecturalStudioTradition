import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AboutComponent } from '@app/modules/about/about.component';
import { routes as AccountRoutes } from '@app/modules/account/account.routes';
import { routes as ArchitectureRoutes } from '@app/modules/architecture/architecture.routes';
import { routes as BlogRoutes } from '@app/modules/blog/blog.routes';
import { HomeComponent } from '@app/modules/home/home.component';
import { routes as InteriorsRoutes } from '@app/modules/interiors/interiors.routes';

import { NotFoundPageComponent } from '@core/pages/not-found-page/not-found-page.component';
import { TermsConditionsPageComponent } from '@core/pages/terms-conditions/terms-conditions.component';
import { NetworkAwarePreloadingService } from '@core/services/network-aware-preloading.service';
import { Page } from '@shared/page.enum';

const routes: Routes = [
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
    RouterModule.forRoot(routes, {
      useHash: true,
      initialNavigation: 'enabledBlocking',
      preloadingStrategy: NetworkAwarePreloadingService
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
