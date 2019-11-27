import { Component } from '@angular/core';
import { ResponsePrintingEditionModel } from '../models/response/response.printingEdition.model';
import { PrintingEditionService } from '../services/printingEdition.service';
import { PostCreateRequestPrintingEditionModel } from '../models/request/postCreate.request.printingEdition.model';

@Component({
  selector: 'app-printingEdition',
  templateUrl: './printingEdition.component.html',
  styleUrls: ['./printingEdition.component.scss']
})
export class PrintingEditionComponent {
  postCreateRequestPrintingEditionModel : PostCreateRequestPrintingEditionModel = {};
  responsePrintingEditionModel : ResponsePrintingEditionModel = {};
  printingEditionId : string;

  constructor(private printingEditionService: PrintingEditionService) { }

  public async getAll(){
    this.responsePrintingEditionModel = await this.printingEditionService.getAll();
  }

  public async getAllWithoutRemove(){
    this.responsePrintingEditionModel = await this.printingEditionService.getAllWithoutRemove();
  }

  public async getPagination(){
    this.responsePrintingEditionModel = await this.printingEditionService.getPagination();
  }

  public async getById(){
    this.responsePrintingEditionModel = await this.printingEditionService.getById(this.printingEditionId);
  }

  public async getBuy(){
    this.responsePrintingEditionModel = await this.printingEditionService.getBuy(this.printingEditionId);
  }

  public async getSort(){
    this.responsePrintingEditionModel = await this.printingEditionService.getSort();
  }

  public async getFiltration(){
    this.responsePrintingEditionModel = await this.printingEditionService.getFiltration();
  }

  public async postCreate(){
    this.responsePrintingEditionModel = await this.printingEditionService.postCreate(this.postCreateRequestPrintingEditionModel);
  }

  public async putUpdate(){
    this.responsePrintingEditionModel = await this.printingEditionService.putUpdate(this.postCreateRequestPrintingEditionModel, this.printingEditionId);
  }

  public async delete(){
    this.responsePrintingEditionModel = await this.printingEditionService.delete(this.printingEditionId);
  }

}