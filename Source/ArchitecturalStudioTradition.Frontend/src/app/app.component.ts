import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';

import { Subject } from 'rxjs';

import ScrollTriggerPlugin from '@app/vendor/smooth-scrollbar/ScrollTriggerPlugin';
import SoftScrollPlugin from '@app/vendor/smooth-scrollbar/SoftScrollPlugin';
import { ConnectionService } from 'angular-connection-service';
import SmoothScrollbar from 'smooth-scrollbar';

SmoothScrollbar.use(ScrollTriggerPlugin, SoftScrollPlugin);

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy {

  private destroy$: Subject<void> = new Subject();

  hasNetworkConnection = true;
  hasInternetAccess = true;

  constructor(
    @Inject(DOCUMENT) private readonly document: Document,
    private readonly connectionService: ConnectionService
  ) {
    this.connectionService.monitor().subscribe((currentState: any) => {
      this.hasNetworkConnection = currentState.hasNetworkConnection;
      this.hasInternetAccess = currentState.hasInternetAccess;
    });
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
    this.destroy$.next();
    this.destroy$.complete();
  }
}

if (typeof Worker !== 'undefined') {
  // Create a new
  const worker = new Worker(new URL('./app.worker', import.meta.url));
  worker.onmessage = ({ data }) => {
    console.log(`page got message: ${data}`);
  };
  worker.postMessage('hello');
} else {
  // Web Workers are not supported in this environment.
  // You should add a fallback so that your program still executes correctly.
}
