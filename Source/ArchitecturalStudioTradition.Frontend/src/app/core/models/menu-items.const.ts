import { Icons } from '@core/models/icons.const';
import { Page } from '@shared/page.enum';
import { HeaderAction } from '@shared/header-action.enum';

export interface MenuItem {
  text?: string;
  link?: string;
  type?: MenuItemType;
  headerAction?: HeaderAction;
  icon?: string;
  active?: boolean;
  position?: 'left' | 'right';
  textPosition?: 'left' | 'right';
  disabled?: boolean;
  expanded?: boolean;
  children?: MenuItem[];
}
export type MenuItemType = 'icon' | 'item' | 'label';

export interface MenuItems {
  iconsBar: MenuItem[];
  header: MenuItem[];
  logo: MenuItem;
}

export const MenuItems: MenuItems = {
  iconsBar: [
    {
      headerAction: HeaderAction.SwitchLanguage,
      icon: Icons.Globe,
      position: 'right'
    },
    {
      headerAction: HeaderAction.SearchOverlay,
      icon: Icons.Magnifier,
      position: 'right'
    }
  ],
  header: [
    {
      text: 'About',
      link: `${Page.About}`,
      position: 'left'
    },
    {
      text: 'Interiors',
      link: `${Page.Interiors}`,
      position: 'left'
    },
    {
      text: 'Architecture',
      link: `${Page.Architecture}`,
      position: 'left'
    },
    {
      text: 'Blog',
      link: `${Page.Blog}`,
      position: 'right'
    },
    {
      text: 'Team',
      link: `${Page.Team}`,
      position: 'right'
    },
    {
      text: 'Contact',
      link: `${Page.Contact}`,
      position: 'right'
    }
  ],
  logo: {
    link: '/'
  }
};
