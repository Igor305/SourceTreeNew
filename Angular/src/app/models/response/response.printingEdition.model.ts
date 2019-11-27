import { BaseResponseModel } from './base.response.model';

export interface ResponsePrintingEditionModel extends BaseResponseModel {
    printingEditionModel? : PrintingEditionModel[]
}
 
export interface PrintingEditionModel{
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