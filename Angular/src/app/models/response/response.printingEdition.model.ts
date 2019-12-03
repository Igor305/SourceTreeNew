import { BaseResponseModel } from './base.response.model';

export interface ResponsePrintingEditionModel extends BaseResponseModel {
    printingEditionModels? : PrintingEditionModels[]
}
 
export interface PrintingEditionModels{
    id? : string;
    name? : string;
    description? : string;
    price? : number;
    status? : string;
    currency? : string;
    createDateTime? : Date;
    updateDateTime? : Date;
    isDeleted? : boolean;   
}