import { BaseResponseModel } from './base.response.model';

export interface PostResponseRefreshTokenModel extends BaseResponseModel{
    accessToken?: string;
    refreshToken?: string;
}