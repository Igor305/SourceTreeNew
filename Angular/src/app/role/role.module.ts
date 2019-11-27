import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoleComponent } from './role.component';

@NgModule({
  declarations: [RoleComponent],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [RoleComponent]
})
export class RoleModule { }
