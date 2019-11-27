import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResponseUserModel } from '../models/response/response.user.model';
import { PostCreateRequestUserModel } from '../models/request/postCreate.request.user.model';
import { environment } from  '../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    
    constructor(private http: HttpClient) { }

    public async getAll(): Promise<ResponseUserModel> {
        const responseUserModel: ResponseUserModel = await this.http.get<ResponseUserModel>(
            environment.protocol + environment.host + environment.port + 
            environment.account + environment.getall).toPromise();

        return responseUserModel;
    }

    public async getAllWithoutRemove(): Promise<ResponseUserModel> {
        const responseUserModel: ResponseUserModel = await this.http.get<ResponseUserModel>(
            environment.protocol + environment.host + environment.port + 
            environment.account + environment.getallwithoutremove).toPromise();

        return responseUserModel;
    }

    public async postCreate(postCreateRequestUserModel: PostCreateRequestUserModel): Promise<ResponseUserModel> {
        const responseUserModel = await this.http.post<ResponseUserModel>(
            environment.protocol + environment.host + environment.port + 
            environment.account + environment.createUser, postCreateRequestUserModel).toPromise();

        return responseUserModel;
    }

    public async putUpdate(postCreateRequestUserModel : PostCreateRequestUserModel, userId: string): Promise<ResponseUserModel> {
        const responseUserModel = await this.http.put<ResponseUserModel>(
            environment.protocol + environment.host + environment.port + 
            environment.account + '/' + userId, postCreateRequestUserModel).toPromise();

        return responseUserModel;
    }

    public async deleteMarkAsDeleted(userId : string) : Promise<ResponseUserModel> {
        const responseUserModel = await this.http.delete<ResponseUserModel>(
            environment.protocol + environment.host + environment.port + 
            environment.account + environment.deleteUser + '/' + userId). toPromise();

        return responseUserModel;
    }

    public async deleteFinalRemoval(userId : string) : Promise<ResponseUserModel> {
        const responseUserModel = await this.http.delete<ResponseUserModel>(
            environment.protocol + environment.host + environment.port + 
            environment.account + environment.finalRemovalUser + '/' + userId). toPromise();

        return responseUserModel;
    }
}