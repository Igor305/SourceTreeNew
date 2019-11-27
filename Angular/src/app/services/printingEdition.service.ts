import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ResponsePrintingEditionModel } from '../models/response/response.printingEdition.model';
import { PostCreateRequestPrintingEditionModel } from  '../models/request/postCreate.request.printingEdition.model';

@Injectable({
  providedIn: 'root'
})
export class PrintingEditionService {

  constructor(private http: HttpClient) { }

  public async getAll() : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment.getall).toPromise();

    return responsePrintingEditionModel;
  }

  public async getAllWithoutRemove () : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment. getallwithoutremove).toPromise();

    return responsePrintingEditionModel;
  }

  public async getPagination () : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment.pagination).toPromise();

      return responsePrintingEditionModel;
  }

  public async getById (printingEditionId : string) : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + printingEditionId).toPromise();
      
      return responsePrintingEditionModel;
  }

  public async getBuy (printingEditionId : string) : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment.buy + printingEditionId).toPromise();
      
      return responsePrintingEditionModel;
  }

  public async getSort () : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment.sort).toPromise();
      
      return responsePrintingEditionModel;
  }

  public async getFiltration () : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.get<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment.filtration).toPromise();
      
      return responsePrintingEditionModel;
  }

  public async postCreate (postCreateRequestPrintingEditionModel : PostCreateRequestPrintingEditionModel) : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.post<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + environment.create, postCreateRequestPrintingEditionModel).toPromise();

      return responsePrintingEditionModel;
  }

  public async putUpdate (postCreateRequestPrintingEditionModel : PostCreateRequestPrintingEditionModel, printingEditionId: string) : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.put<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + printingEditionId, postCreateRequestPrintingEditionModel).toPromise();

      return responsePrintingEditionModel;
    }

  public async delete (printingEditionId: string) : Promise<ResponsePrintingEditionModel>{
    const responsePrintingEditionModel : ResponsePrintingEditionModel = await this.http.delete<ResponsePrintingEditionModel>(
      environment.protocol + environment.host + environment.port +
      environment.printingEdition + printingEditionId).toPromise();

      return responsePrintingEditionModel;
  }
}
