import { BaseResponseModel } from './base.response.model';

export interface ResponseRoleModel extends BaseResponseModel{
    roleModel?: RoleModel []
}

export interface RoleModel {
    id?: string;
    name?: string;
    normalizedName?: string;
    concurrencyStamp?: string;
}