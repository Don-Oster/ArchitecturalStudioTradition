import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { User } from '@shared/models/user.model';

import { MenuItem } from '@core/models/menu-items.const';
import { AuthenticationService } from '@core/services/authentication/authentication.service';

@Component({
  selector: 'app-user-icon',
  templateUrl: './user-icon.component.html',
  styleUrls: ['./user-icon.component.scss']
})
export class UserIconComponent implements OnInit {

  @Input() user: User;

  public item: MenuItem = {
    icon: 'user',
    text: 'Login or create an account'
  };

  public isDropDownActive: boolean = false;

  constructor(
    private readonly authService: AuthenticationService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    if (this.user && this.authService.isUserAuthenticated()) {
      this.item.text = this.user.name;
    }
  }

  public dropDownToggled(isActive: boolean) {
    this.isDropDownActive = isActive;
  }

  public logIn() {
    this.router.navigate(['/accounts/login']);
  }

  public logOut() {
    this.authService.logout();
  }

}
