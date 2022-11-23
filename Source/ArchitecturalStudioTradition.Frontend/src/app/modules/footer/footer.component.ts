import { footerItems, socialItems } from './footer.const';
import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  items = footerItems;
  social = socialItems;

  year = new Date().getFullYear();
}
