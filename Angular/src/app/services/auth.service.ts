import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
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

    public async registration(postRequestRegisterModel: PostRequestRegisterModel) {
        const url: string = `${environment.protocol}${environment.host}${environment.port}${environment.auth}${environment.singUp}`;      
        const registration = await this.http.post<BaseResponseModel>(url,postRequestRegisterModel).toPromise();

        if(!registration.status){
            environment.message = registration.error;
        }
        if(registration.status){
        environment.message.push(registration.message);
        }
        
        return environment.message;
    }

    public async login(postRequestLoginModel: PostRequestLoginModel): Promise<PostResponseLoginModel> {
        const options = this.header();
        const postResponseLoginModel = await this.http.post<PostResponseLoginModel>(
            environment.protocol + environment.host + environment.port +
            environment.auth + environment.singIn, postRequestLoginModel).toPromise();
        if (postResponseLoginModel.status) {
            this.router.navigate([""]);
            localStorage.setItem('accessToken', postResponseLoginModel.accessToken);
            localStorage.setItem('refreshToken', postResponseLoginModel.refreshToken);
            options.headers.set('Authorization', 'Bearer ${postResponseLoginModel.accessToken}');
        }
        return postResponseLoginModel;
    }

    private header() {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json'
        })
        const options = { headers: headers };
        return options;
    }

    public async forgotPassword(postRequestForgotPasswordModel: PostRequestForgotPasswordModel): Promise<BaseResponseModel> {
        const baseResponseModel: BaseResponseModel = await this.http.post<BaseResponseModel>(
            environment.protocol + environment.host + environment.port +
            environment.auth + environment.forgotPassword, postRequestForgotPasswordModel).toPromise();

        return baseResponseModel;
    }

    public async resetPassword(postRequestResetPasswordModel: PostRequestResetPasswordModel): Promise<BaseResponseModel> {
        const baseResponseModel: BaseResponseModel = await this.http.post<BaseResponseModel>(
            environment.protocol + environment.host + environment.port +
            environment.auth + environment.resetPassword, postRequestResetPasswordModel).toPromise();

        return baseResponseModel;
    }

    public async refreshToken(postRequestRefreshTokenModel: PostRequestRefreshTokenModel): Promise<PostResponseRefreshTokenModel> {
        const postResponseRefreshTokenModel: PostResponseRefreshTokenModel = await this.http.post<PostResponseRefreshTokenModel>(
            environment.protocol + environment.host + environment.port +
            environment.auth + environment.refreshToken, postRequestRefreshTokenModel).toPromise();

        return postResponseRefreshTokenModel;
    }

    public userData(token: string) {
        const helper = new JwtHelperService();
        const decodeaccesstoken = helper.decodeToken(token);
        const Email = decodeaccesstoken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
        const isAdmin = decodeaccesstoken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin';
        localStorage.setItem('Email', Email.toString());
        localStorage.setItem('isAdmin', isAdmin.toString());
    }

    public isExpared(token: string): boolean {
        const helper = new JwtHelperService();
        const refreshtokenValisTo = helper.getTokenExpirationDate(token);
        const displayDate = new Date();
        const isExpired = refreshtokenValisTo > displayDate;
        return isExpired;
    }
}

