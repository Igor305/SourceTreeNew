import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { AuthorService } from 'src/app/services';
import { PrintingEditionService } from 'src/app/services/printingEdition.service';

import { ResponsePrintingEditionModel } from 'src/app/models/response/response.printingEdition.model';
import { PostCreateRequestPrintingEditionModel } from 'src/app/models/request/postCreate.request.printingEdition.model';
import { PostAddImageRequestPrintingEditionModel } from 'src/app/models/request/postAddImage.request.printingEdition.model';
import { BaseResponseModel } from 'src/app/models/response/base.response.model';
import { ResponseAuthorModel, AuthorModel } from 'src/app/models/response/response.author.model';


@Component({
  selector: 'app-create-printing-edition',
  templateUrl: './create-printing-edition.component.html',
  styleUrls: ['./create-printing-edition.component.scss']
})
export class CreatePrintingEditionComponent implements OnInit {

  postCreateRequestPrintingEditionModel: PostCreateRequestPrintingEditionModel = {};
  authors: AuthorModel[] = [];
  image : string;

  constructor(private printingEditionService: PrintingEditionService, private authorService : AuthorService) { }

  createPEForm : FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    type: new FormControl('', Validators.required),
    status: new FormControl('', Validators.required),
    currency: new FormControl('', Validators.required)
  });

  private async getBase64(event : Event) {
    
    const file: HTMLInputElement = event.target as HTMLInputElement;

    if (file && file.files.length){
    const fileData = file.files[0];
    const reader = new FileReader();
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

  private async postCreate(){
    const responsePrintingEditionModel : ResponseAuthorModel = await this.printingEditionService.postCreate(this.postCreateRequestPrintingEditionModel);
    if (this.image !== ''){
      const postAddImageRequestPrintingEditionModel : PostAddImageRequestPrintingEditionModel ={};
      postAddImageRequestPrintingEditionModel.image = this.image;
      const responsePrintingEditionModel:  ResponsePrintingEditionModel = {};
      responsePrintingEditionModel.printingEditionModels.forEach(printingEdition => postAddImageRequestPrintingEditionModel.printingEditionId = printingEdition.id );
      const baseResponseModel: BaseResponseModel = await this.printingEditionService.postAddImage(postAddImageRequestPrintingEditionModel);
    }
  }

  private async getAllWithoutRemove(){
    const responseAuthorModel : ResponseAuthorModel = await this.authorService.getAllWithoutRemove();
    const authorModels  = responseAuthorModel.authorModel;
    return authorModels;
  }

 async ngOnInit (){
    this.authors = await this.getAllWithoutRemove();
  }
}
