import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule }   from '@angular/forms';
import { RouterModule } from '@angular/router';

import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';


@NgModule({
  declarations: [
    RegistrationComponent,
    LoginComponent,
    ForgotPasswordComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([{
          path: '', children : [
          { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
          { path: 'registration', component: RegistrationComponent },
          { path: 'login', component : LoginComponent }, 
          { path: 'forgotPassword', component : ForgotPasswordComponent },
        ]
      }
    ])
  ],
  exports: [RouterModule, FormsModule]
})
export class AuthModule { }
