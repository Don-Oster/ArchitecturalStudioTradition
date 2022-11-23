import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Subject } from 'rxjs';

import { ConnectionService } from 'angular-connection-service';

import SmoothScrollbar from 'smooth-scrollbar';
import ScrollTriggerPlugin from '@app/vendor/smooth-scrollbar/ScrollTriggerPlugin';
import SoftScrollPlugin from '@app/vendor/smooth-scrollbar/SoftScrollPlugin';

SmoothScrollbar.use(ScrollTriggerPlugin, SoftScrollPlugin);

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  private destroy$: Subject<void> = new Subject();

  hasNetworkConnection: boolean = true;
  hasInternetAccess: boolean = true;

  constructor(
    @Inject(DOCUMENT) private readonly document: Document,
    private readonly connectionService: ConnectionService
  ) {
    this.connectionService.monitor().subscribe((currentState: any) => {
      this.hasNetworkConnection = currentState.hasNetworkConnection;
      this.hasInternetAccess = currentState.hasInternetAccess;
    });
}

  ngOnInit() {
  }

  ngAfterViewInit() {
    const view = this.document.getElementById('view-main');
    SmoothScrollbar.init(view, {
      renderByPixels: false,
      damping: 0.075
    });
  }

  isOnline() {
    return this.hasNetworkConnection && this.hasInternetAccess;
  }

  ngOnDestroy() {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
