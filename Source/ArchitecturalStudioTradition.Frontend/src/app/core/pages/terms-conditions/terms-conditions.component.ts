import { Component, OnInit } from '@angular/core';

import { PageMetaService } from '@shared/services/meta-tags/page-meta.service';
import { GoogleAnalyticsService } from '@shared/services/google-analytics/google-analytics.service';

@Component({
  selector: 'app-terms-conditions',
  templateUrl: './terms-conditions.component.html',
  styleUrls: ['./terms-conditions.component.scss']
})
export class TermsConditionsPageComponent implements OnInit {

  constructor(
    private readonly pageMeta: PageMetaService,
    private readonly googleAnalytics: GoogleAnalyticsService
  ) { }

  ngOnInit() {
    this.pageMeta.forTermsConditions();
    this.googleAnalytics.pageView();
  }

}
