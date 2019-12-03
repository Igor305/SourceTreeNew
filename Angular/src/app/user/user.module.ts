import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserComponent } from './user.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [UserComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([{
      path: '', children : [
        { path: '', redirectTo: '/user/user', pathMatch: 'full' },
        { path: 'user', component: UserComponent },
      ]
    }
  ])
],
  exports: [RouterModule]
})
export class UserModule { }
