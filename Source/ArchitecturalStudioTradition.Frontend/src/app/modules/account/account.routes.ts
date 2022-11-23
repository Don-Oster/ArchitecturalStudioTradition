import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import { RegisterComponent } from './pages/register/register.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  //{ path: 'reset-password', component: ResetPasswordComponent },
  //{ path: 'email-confirmation', component: EmailConfirmationComponent },
  //{ path: 'two-step-verification', component: TwoStepVerificationComponent }
];

