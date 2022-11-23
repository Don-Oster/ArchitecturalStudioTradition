import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { SocialUser } from '@abacritt/angularx-social-login';

import { ReCaptchaV3Service } from 'ngx-captcha';

import { AuthenticationResponse } from '@archtradition-contract';
import { environment } from '@environment';
import { AuthenticationService } from '@core/services/authentication/authentication.service';
import { Page } from '@shared/page.enum';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  private returnUrl: string;

  user: SocialUser | undefined;
  Page = Page;
  logInForm: FormGroup;
  isLoading: boolean = false;
  passwordIsInvalid: boolean = false;

  constructor(
    private readonly authService: AuthenticationService,
    private readonly reCaptchaV3Service: ReCaptchaV3Service,
    private readonly router: Router,
    private readonly route: ActivatedRoute) { }

  ngOnInit() {
    this.authService.externalAuthChanged.subscribe((user) => {
      this.user = user;
    });

    this.logInForm = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, [Validators.required])
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  logIn() {
    this.isLoading = true;
    this.checkReCaptcha();
    this.loginUser();
  }

  logInWithFacebook() {
    this.authService.signInWithFacebook();
  }

  logInWithGoogle() {
    this.authService.signInWithGoogle();
  }

  logOut(): void {
    this.authService.logout();
  }

  refreshGoogleToken() {
    this.authService.refreshGoogleToken();
  }

  checkReCaptcha() {
    this.reCaptchaV3Service.execute(environment.recaptcha.siteKey, 'account/login', (token) => {
      console.log('This is your token: ', token);
    }, {
      useGlobalDomain: false
    });
  }

  isDataInvalid(data: string) {
    return (this.logInForm.get(data).touched && !this.logInForm.get(data).valid);
  }

  get isEmailInvalid() {
    return this.isDataInvalid('email');
  }

  get isPasswordInvalid() {
    return this.isDataInvalid('password');
  }

  private loginUser() {
    this.authService.loginUser({ ...this.logInForm.value, clientUri: '/accounts/forgot-password' })
      .subscribe({
        next: (response: AuthenticationResponse) => {
          if (response.is2StepVerificationRequired) {
            this.router.navigate(['/accounts/two-step-verification'],
              { queryParams: { returnUrl: this.returnUrl, provider: response.provider, email: this.logInForm.get('email').value } })
          }
          else {
            localStorage.setItem('token', response.token);
            this.authService.sendAuthStateChangeNotification(response.isSuccessful);
            this.router.navigate([this.returnUrl]);
          }
        },
        //error: (err: HttpErrorResponse) => {
        //  this.errorMessage = err.message;
        //  this.showError = true;
        //}
      });
  }
}
