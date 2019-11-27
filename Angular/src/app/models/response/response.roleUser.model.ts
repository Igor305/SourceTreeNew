import { BaseResponseModel } from './base.response.model';

export interface ResponseRoleUserModel extends BaseResponseModel{
    roleUserModel? : RoleUserModel []
}

export interface RoleUserModel{
    userId? : string;
    roleId? : string;
}