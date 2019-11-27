import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { PostRequestRegisterModel } from '../models/request/post.request.register.model';
import { BaseResponseModel } from '../models/response/base.response.model';
import { PostRequestLoginModel } from '../models/request/post.request.login.model';
import { PostResponseLoginModel } from '../models/response/post.response.login.model';
import { PostRequestResetPasswordModel } from '../models/request/post.request.resetPassword.model';
import { PostRequestForgotPasswordModel } from '../models/request/post.request.forgotPassword.model';
import { PostResponseRefreshTokenModel } from '../models/response/post.response.refreshToken.model';
import { PostRequestRefreshTokenModel } from '../models/request/post.request.refreshToken.model';

@Injectable({
providedIn: 'root'
})
export class AuthService {

constructor(private http: HttpClient, private router: Router) { }

    public async registration(postRequestRegisterModel : PostRequestRegisterModel) : Promise<any[]> {
        const registration = await this.http.post<BaseResponseModel>( 
            environment.protocol + environment.host + environment.port +
            environment.auth + environment.singUp, postRequestRegisterModel).toPromise();
        registration.status ? environment.message.join(registration.message) : environment.message = registration.error;
        return environment.message;
    }

    public async login (postRequestLoginModel : PostRequestLoginModel) : Promise<PostResponseLoginModel>{      
        const options = this.header(); 
        const postResponseLoginModel = await this.http.post<PostResponseLoginModel>(
            environment.protocol + environment.host + environment.port +
            environment.auth + environment.singIn, postRequestLoginModel).toPromise();
        if (postResponseLoginModel.status){
            this.router.navigate ([""]);
            localStorage.setItem ('accessToken', postResponseLoginModel.accessToken);
            localStorage.setItem ('refreshToken', postResponseLoginModel.refreshToken);
            options.headers.set('Authorization', 'Bearer ${postResponseLoginModel.accessToken}');
        }
        return postResponseLoginModel;
    }
    
    private header(){
        const headers = new HttpHeaders({
            'Content-Type': 'application/json'}) 
        const options = { headers: headers };
        return options;
    }

    public async forgotPassword(postRequestForgotPasswordModel : PostRequestForgotPasswordModel) : Promise<BaseResponseModel>{
        const baseResponseModel : BaseResponseModel = await this.http.post<BaseResponseModel>(
        environment.protocol + environment.host + environment.port +
        environment.auth + environment.forgotPassword, postRequestForgotPasswordModel).toPromise();

        return baseResponseModel;
    }

    public async resetPassword(postRequestResetPasswordModel : PostRequestResetPasswordModel) : Promise <BaseResponseModel>{
        const baseResponseModel : BaseResponseModel = await this.http.post<BaseResponseModel>(
        environment.protocol + environment.host + environment.port +
        environment.auth + environment.resetPassword, postRequestResetPasswordModel).toPromise();

        return baseResponseModel;
    }

    public async refreshToken(postRequestRefreshTokenModel: PostRequestRefreshTokenModel) : Promise <PostResponseRefreshTokenModel>{
        const postResponseRefreshTokenModel : PostResponseRefreshTokenModel = await this.http.post<PostResponseRefreshTokenModel>(
        environment.protocol + environment.host + environment.port +
        environment.auth + environment.refreshToken, postRequestRefreshTokenModel).toPromise();

        return postResponseRefreshTokenModel;
    }
}

   