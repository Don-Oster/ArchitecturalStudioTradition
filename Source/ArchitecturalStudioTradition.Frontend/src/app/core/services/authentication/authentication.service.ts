import { Injectable } from '@angular/core';

import { Observable, Subject } from 'rxjs';

import { JwtHelperService } from '@auth0/angular-jwt';
import {
  AccountsService,
  RegisterCommand,
  LoginCommand,
  ExternalLoginCommand,
  ForgotPasswordCommand,
  ResetPasswordCommand,
  TwoStepVerificationCommand,
  TokenResponse,
  RegistrationResponse,
  AuthenticationResponse
} from '@archtradition-contract';
import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { GoogleLoginProvider, FacebookLoginProvider } from "@abacritt/angularx-social-login";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private authChange$ = new Subject<boolean>();
  private externalAuthChange = new Subject<SocialUser>();

  authChanged = this.authChange$.asObservable();
  externalAuthChanged = this.externalAuthChange.asObservable();
  isExternalAuth: boolean;

  constructor(
    private readonly accountsService: AccountsService,
    private readonly jwtHelper: JwtHelperService,
    private readonly externalAuthService: SocialAuthService) {
    this.externalAuthService.authState.subscribe((user) => {
      this.externalAuthChange.next(user);
      this.isExternalAuth = true;
    })
  }

  registerUser = (command: RegisterCommand): Observable<RegistrationResponse> => {
    return this.accountsService.registerUser(command);
  }

  loginUser = (command: LoginCommand): Observable<AuthenticationResponse> => {
    return this.accountsService.login(command);
  }

  forgotPassword = (command: ForgotPasswordCommand) => {
    return this.accountsService.forgotPassword(command);
  }

  resetPassword = (command: ResetPasswordCommand) => {
    return this.accountsService.resetPassword(command);
  }

  twoStepVerification = (command: TwoStepVerificationCommand): Observable<TokenResponse> => {
    return this.accountsService.twoStepVerification(command);
  }

  externalLogin = (command: ExternalLoginCommand): Observable<AuthenticationResponse> => {
    return this.accountsService.externalLogin(command);
  }

  confirmEmail = (token: string, email: string) => {
    return this.accountsService.emailConfirmation(token, email);
  }

  sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChange$.next(isAuthenticated);
  }

  logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");

    return token && !this.jwtHelper.isTokenExpired(token);
  }

  isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']

    return role === 'Administrator';
  }

  signInWithFacebook = () => {
    this.externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signInWithGoogle = () => {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  refreshGoogleToken = () => {
    this.externalAuthService.refreshAuthToken(GoogleLoginProvider.PROVIDER_ID);
  }

  signOutExternal = () => {
    this.externalAuthService.signOut();
  }
}
