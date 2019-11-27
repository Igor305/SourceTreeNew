import { BaseResponseModel } from './base.response.model';

export interface ResponseAuthorModel extends BaseResponseModel {
    authorModel? : AuthorModel []
}

export interface AuthorModel {
    id?: string,
    firstName?: string,
    lastName?: string,
    dateBirth?: Date,
    dateDeath?: Date,
    createDateTime?: Date,
    updateDateTime?: Date,
    isDeleted?: boolean
}