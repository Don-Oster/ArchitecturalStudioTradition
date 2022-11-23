import { Component, OnInit } from '@angular/core';

import { GoogleAnalyticsService } from '@shared/services/google-analytics/google-analytics.service';
import { PageMetaService } from '@shared/services/meta-tags/page-meta.service';

@Component({
  selector: 'app-not-found-page',
  templateUrl: './not-found-page.component.html',
  styleUrls: ['./not-found-page.component.scss']
})
export class NotFoundPageComponent implements OnInit {

  constructor(
    private readonly pageMeta: PageMetaService,
    private readonly googleAnalytics: GoogleAnalyticsService,
  ) { }

  ngOnInit() {
    this.pageMeta.forNotFound();
    this.googleAnalytics.pageView();
  }

}
