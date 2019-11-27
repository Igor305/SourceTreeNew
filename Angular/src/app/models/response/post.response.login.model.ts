import { BaseResponseModel } from './base.response.model';

export interface PostResponseLoginModel extends BaseResponseModel {
    accessToken? : string;
    refreshToken? : string;
}