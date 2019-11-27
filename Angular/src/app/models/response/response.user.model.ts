import { BaseResponseModel } from './base.response.model';

export interface ResponseUserModel extends BaseResponseModel {
    userModels?: UserModel[]
}

export interface UserModel {
    id?: string;
    email?: string;
    firstName?: string;
    lastName?: string;
    phoneNumber?: string;
    createDateTime?: string;
    updateDateTime?: string;
    isDeleted?: boolean;
}