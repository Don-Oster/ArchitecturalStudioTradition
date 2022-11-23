import { Component, Input } from '@angular/core';

import { MenuItems, MenuItem } from '@core/models/menu-items.const';
import { User } from '@shared/models/user.model';

@Component({
  selector: 'app-menu-mobile',
  templateUrl: './menu-mobile.component.html',
  styleUrls: ['./menu-mobile.component.scss']
})
export class MenuMobileComponent {

  @Input() set menuItems(menuItems: MenuItems) {
    if (menuItems) {
      this.header.push(...menuItems.header);
      this.iconsBar.push(...menuItems.iconsBar.filter(x => x.text.toLocaleLowerCase() !== 'Search'));
    }
  }
  @Input() user: User;

  header: MenuItem[] = [];
  iconsBar: MenuItem[] = [];
  isVisible = false;
}
