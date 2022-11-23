import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { ReCaptchaV3Service } from 'ngx-captcha';

import { environment } from '@environment';
import { AuthenticationService } from '@core/services/authentication/authentication.service';
import { Page } from '@shared/page.enum';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
})
export class ForgotPasswordComponent implements OnInit {

  private returnUrl: string;

  Page = Page;
  recoverPasswordForm: FormGroup;
  isLoading: boolean = false;

  constructor(
    private readonly authService: AuthenticationService,
    private readonly reCaptchaV3Service: ReCaptchaV3Service,
    private readonly route: ActivatedRoute) { }

  ngOnInit() {
    this.recoverPasswordForm = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  recoverPassword() {
    this.isLoading = true;
    this.checkReCaptcha();
    this.authService.forgotPassword({ ...this.recoverPasswordForm.value, clientUri: this.returnUrl });
  }

  checkReCaptcha() {
    this.reCaptchaV3Service.execute(environment.recaptcha.siteKey, 'account/recover-password', (token) => {
      console.log('This is your token: ', token);
    }, {
      useGlobalDomain: false
    });
  }

  isDataInvalid(data: string) {
    return (this.recoverPasswordForm.get(data).touched && !this.recoverPasswordForm.get(data).valid);
  }

  get isEmailInvalid() {
    return this.isDataInvalid('email');
  }
}
