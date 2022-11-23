import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';

import { fromEvent, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { MenuItem, MenuItems } from '@core/models/menu-items.const';

import { Viewport } from '@shared/viewport/viewport.const';
import { User } from '@shared/models/user.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  private destroy$: Subject<void> = new Subject();

  @Input() user: User;
  @Output() menuItemClickedEvent: EventEmitter<MenuItem> = new EventEmitter();

  menuItems: MenuItems = MenuItems;
  menuType: 'desktop' | 'mobile' = 'desktop';
  mobileWidth = Viewport.Mobile;

  ngOnInit() {
    this.menuType = window.innerWidth < this.mobileWidth ? 'mobile' : 'desktop';
    fromEvent(window, 'resize')
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => this.menuType = window.innerWidth < this.mobileWidth ? 'mobile' : 'desktop');
  }

  ngOnDestroy() {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
