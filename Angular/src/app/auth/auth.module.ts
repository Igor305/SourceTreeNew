import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }   from '@angular/forms';
import { RouterModule } from '@angular/router';

import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';


@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
    ForgotPasswordComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([{
          path: '', children : [
          { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
          { path: 'registration', component: RegisterComponent },
          { path: 'login', component : LoginComponent }, 
          { path: 'forgotPassword', component : ForgotPasswordComponent },
        ]
      }
    ])
  ],
  exports: [RouterModule]
})
export class AuthModule { }
