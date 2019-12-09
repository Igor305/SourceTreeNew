import { Component, OnInit } from '@angular/core';
import { ResponsePrintingEditionModel } from '../models/response/response.printingEdition.model';
import { PrintingEditionService } from '../services/printingEdition.service';
import { PostCreateRequestPrintingEditionModel } from '../models/request/postCreate.request.printingEdition.model';

@Component({
  selector: 'app-printing-edition',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.scss']
})
export class PrintingEditionComponent implements OnInit {
  postCreateRequestPrintingEditionModel : PostCreateRequestPrintingEditionModel = {};
  responsePrintingEditionModel : ResponsePrintingEditionModel = {};
  printingEditionId : string;
  image : string;
  
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

  public async putUpdate(){
    this.responsePrintingEditionModel = await this.printingEditionService.putUpdate(this.postCreateRequestPrintingEditionModel, this.printingEditionId);
  }

  public async delete(){
    this.responsePrintingEditionModel = await this.printingEditionService.delete(this.printingEditionId);
  }

  public async getBase64(event : Event) {
    
    let file: HTMLInputElement = event.target as HTMLInputElement;

    if (file && file.files.length){
    let fileData = file.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(fileData);
    reader.onload = () => {
      this.image = reader.result.toString();
      console.log(this.image);
    };
    
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
  }
}
 

  ngOnInit(){
    this.getAllWithoutRemove();
    console.log(this.getAllWithoutRemove());
  }
}