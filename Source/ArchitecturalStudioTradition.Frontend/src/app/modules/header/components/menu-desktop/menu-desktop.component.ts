import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

import { Subject } from 'rxjs';
import { filter, map, takeUntil } from 'rxjs/operators';

import { MenuItem } from '@core/models/menu-items.const';
import { Page } from '@shared/page.enum';
import { HeaderAction } from '@shared/header-action.enum';
import { User } from '@shared/models/user.model';

import { SwitchLanguageModalComponent } from '../switch-language-modal/switch-language-modal.component';

@Component({
  selector: 'app-menu-desktop',
  templateUrl: './menu-desktop.component.html',
  styleUrls: ['./menu-desktop.component.scss']
})
export class MenuDesktopComponent implements OnInit, OnDestroy {

  private searchMenuItem: MenuItem;
  private destroy$: Subject<void> = new Subject();

  @Input() menuItems: MenuItem[];
  @Input() logoItem: MenuItem;
  @Input() user: User;

  displaySearchOverlay = false;

  constructor(private readonly router: Router) { }

  ngOnInit() {
    if (this.menuItems) {
      this.searchMenuItem = this.menuItems.find(i => i.headerAction === HeaderAction.SearchOverlay);
    }

    this.router.events
      .pipe(
        filter(event => event instanceof NavigationStart),
        takeUntil(this.destroy$)
      )
      .subscribe((event: NavigationStart) => {
        this.setSearchOverlay(false);
      });

    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(event => event as NavigationEnd),
        takeUntil(this.destroy$)
      ).subscribe(event => {
        this.toggleSearchIconMode(
          event.urlAfterRedirects.startsWith(`/${Page.Search}`)
        );
      });
  }

  handleHeaderAction(item: MenuItem) {
    if (!item.disabled && item.headerAction) {
      switch (item.headerAction) {
        case HeaderAction.SwitchLanguage:
          this.openSwitchLanguageModal();
          break;
        case HeaderAction.SearchOverlay:
          this.setSearchOverlay(!this.displaySearchOverlay);
          break;
      }
    }
  }

  setSearchOverlay(value: boolean) {
    this.displaySearchOverlay = value;
    this.searchMenuItem.active = this.displaySearchOverlay;
  }

  private openSwitchLanguageModal() {
    // this.modalService.open(SwitchLanguageModalComponent, { size: 'md' });
  }

  private toggleSearchIconMode(isSearchPage: boolean = true) {
    this.searchMenuItem.active = isSearchPage;
    this.searchMenuItem.disabled = isSearchPage;
  }

  ngOnDestroy() {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
