import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { ResponseOrderModel } from '../models/response/resposnse.order.model';
import { PostCreateRequestOrderModel } from '../models/request/postCreate.request.order.model';
import { StripeScriptTag } from "stripe-angular";

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  private publishableKey:string = "...YOUR-STRIPE-KEY-HERE..."
 
  constructor(public StripeScriptTag:StripeScriptTag, private orderServise: OrderService){
    this.StripeScriptTag.setPublishableKey( this.publishableKey )
  }

  postCreateRequiredOrderModel : PostCreateRequestOrderModel = {};
  orderId : string;

  public async getAll(){
    const responseOrderModel: ResponseOrderModel = await this.orderServise.getAll();
  }

  public async getAllWithoutRemove(){
    const responseOrderModel: ResponseOrderModel = await this.orderServise.getAllWithoutRemove();
  }

  public async getPagination(){
    const responseOrderModel: ResponseOrderModel = await this.orderServise.getPagination();
  }

  public async getById(){
    const responseOrderModel: ResponseOrderModel = await this.orderServise.getById(this.orderId);
  }

  public async postCreate(){
    const responseOrderModel: ResponseOrderModel = await this.orderServise.postCreate(this.postCreateRequiredOrderModel);
  }
  
  public async delete(){
    const responseOrderModel: ResponseOrderModel = await this.orderServise.delete(this.orderId);
  }
  ngOnInit() {
  }

}
