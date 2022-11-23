import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';

import { ReCaptchaV3Service } from 'ngx-captcha';

import { environment } from '@environment';
import { AuthenticationService } from '@core/services/authentication/authentication.service';
import { Page } from '@shared/page.enum';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  Page = Page;
  registerForm: FormGroup;
  isLoading: boolean = false;
  passwordIsInvalid: boolean = false;

  constructor(
    private readonly authService: AuthenticationService,
    private readonly reCaptchaV3Service: ReCaptchaV3Service,
    private readonly router: Router) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      'firstName': new FormControl(null, [Validators.required]),
      'lastName': new FormControl(null, [Validators.required]),
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, [Validators.required])
    }, { validators: this.agreementsValidator });
  }

  register() {
    this.isLoading = true;
    this.checkReCaptcha();
    this.registerUser();
  }

  checkReCaptcha() {
    this.reCaptchaV3Service.execute(environment.recaptcha.siteKey, 'account/register', (token) => {
      console.log('This is your token: ', token);
    }, {
      useGlobalDomain: false
    });
  }

  agreementsValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    return this.passwordIsInvalid ? { agreementsValid: false } : null;
  };

  passwordValid(isValid: boolean) {
    this.passwordIsInvalid = !isValid;
  }

  isDataInvalid(data: string) {
    return (this.registerForm.get(data).touched && !this.registerForm.get(data).valid);
  }

  get isEmailInvalid() {
    return this.isDataInvalid('email');
  }

  get isPasswordInvalid() {
    return this.isDataInvalid('password') || this.passwordIsInvalid;
  }

  private registerUser() {
    this.authService.registerUser({ ...this.registerForm.value, clientUri: '/accounts/email-confirmation' })
      .subscribe({
        next: (_) => this.router.navigate(["/accounts/login"]),
        //error: (err: HttpErrorResponse) => {
        //  this.errorMessage = err.message;
        //  this.showError = true;
        //}
      })
  }
}
