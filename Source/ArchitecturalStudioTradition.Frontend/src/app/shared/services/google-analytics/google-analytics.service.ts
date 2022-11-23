import { Injectable, NgZone, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { first, takeUntil } from 'rxjs/operators';

import { GoogleTagManagerService } from '../google-tag-manager/google-tag-manager.service';

@Injectable()
export class GoogleAnalyticsService implements OnDestroy {

  private destroy$: Subject<void> = new Subject();

  constructor(
    private readonly googleTagManagerService: GoogleTagManagerService,
    private readonly zone: NgZone,
  ) {}

  pageView() {
    this.zone.onStable
      .pipe(
        first(),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.googleTagManagerService.pushTag({ event: 'pageview' });
      });
  }

  ngOnDestroy() {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
