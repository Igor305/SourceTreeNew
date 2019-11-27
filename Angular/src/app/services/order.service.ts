import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from  '../../environments/environment';
import { ResponseOrderModel } from '../models/response/resposnse.order.model';
import { PostCreateRequestOrderModel } from '../models/request/postCreate.request.order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http : HttpClient) { }
 
  public async getAll (): Promise<ResponseOrderModel>{
    const responseOrderModel : ResponseOrderModel = await this.http.get (
    environment.protocol + environment.host + environment.port +
    environment.order + environment.getall).toPromise();

    return responseOrderModel;
  }

  public async getAllWithoutRemove (): Promise<ResponseOrderModel>{
    const responseOrderModel : ResponseOrderModel = await this.http.get (
    environment.protocol + environment.host + environment.port +
    environment.order + environment.getallwithoutremove).toPromise();

    return responseOrderModel;
  }

  public async getPagination (): Promise<ResponseOrderModel>{
    const responseOrderModel : ResponseOrderModel = await this.http.get (
    environment.protocol + environment.host + environment.port +
    environment.order + environment.pagination).toPromise();

    return responseOrderModel;
  }
  
  public async getById (orderId : string): Promise<ResponseOrderModel>{
    const responseOrderModel : ResponseOrderModel = await this.http.get (
    environment.protocol + environment.host + environment.port +
    environment.order + orderId).toPromise();

    return responseOrderModel;
  }

  public async postCreate (postCreateRequestOrderModel : PostCreateRequestOrderModel): Promise<ResponseOrderModel>{
    const responseOrderModel : ResponseOrderModel = await this.http.post (
    environment.protocol + environment.host + environment.port +
    environment.order + environment.create, postCreateRequestOrderModel ).toPromise();

    return responseOrderModel;
  }

  public async delete (orderId : string): Promise<ResponseOrderModel>{
    const responseOrderModel : ResponseOrderModel = await this.http.delete (
    environment.protocol + environment.host + environment.port +
    environment.order + orderId).toPromise();

    return responseOrderModel;
  }

}
