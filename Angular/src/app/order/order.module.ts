import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { FormsModule } from '@angular/forms';
import { OrderRoutingModule } from './order-routing.module';
import { Module as StripeModule } from "stripe-angular"

@NgModule({
  declarations: [OrderComponent],
  imports: [
    CommonModule,
    FormsModule,
    OrderRoutingModule,
    StripeModule.forRoot() 
  ],
  exports: [OrderComponent],
})
export class OrderModule { }
