import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from  '../../environments/environment';
import { ResponseRoleModel } from '../models/response/response.role.model';
import { PostCreateRequestRoleModel } from '../models/request/postCreate.request.role.model';
import { PostCreateRequestRoleUserModel } from '../models/request/postCreate.request.roleUser.model';
import { ResponseRoleUserModel } from '../models/response/response.roleUser.model';
import { PutRequestRoleModel } from '../models/request/put.request.role.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http : HttpClient) { }

  public async getAllRoles() : Promise<ResponseRoleModel>{
    const responseRoleModel : ResponseRoleModel = await this.http.get<ResponseRoleModel>(
    environment.protocol + environment.host + environment.port + 
    environment.account + environment.getAllRole).toPromise();

    return responseRoleModel;
  }

  public async postCreateRole(postCreateRequestRoleModel : PostCreateRequestRoleModel) : Promise<ResponseRoleModel>{
    const responseRoleModel : ResponseRoleModel = await this.http.post<ResponseRoleModel>(
    environment.protocol + environment.host + environment.port + 
    environment.account + environment.createRole, postCreateRequestRoleModel).toPromise();

    return responseRoleModel;
  }
  
  public async postAddingRoleUser(postCreateRequestRoleUserModel : PostCreateRequestRoleUserModel) : Promise<ResponseRoleUserModel>{
    const responseRoleUserModel : ResponseRoleUserModel = await this.http.post<ResponseRoleUserModel>(
    environment.protocol + environment.host + environment.port + 
    environment.account + environment.addingRoleUser, postCreateRequestRoleUserModel).toPromise();

    return responseRoleUserModel;
  }

  public async postTakingAwayRoleUser(postCreateRequestRoleUserModel : PostCreateRequestRoleUserModel) : Promise<ResponseRoleUserModel>{
    const responseRoleUserModel : ResponseRoleUserModel = await this.http.post<ResponseRoleUserModel>(
    environment.protocol + environment.host + environment.port + 
    environment.account + environment.takingRoleUser, postCreateRequestRoleUserModel).toPromise();
    
    return responseRoleUserModel;
  }

  public async putUpdateRole(putRequestRoleModel : PutRequestRoleModel, name : string) : Promise<ResponseRoleModel>{
    const responseRoleModel : ResponseRoleModel = await this.http.put<ResponseRoleModel>(
    environment.protocol + environment.host + environment.port + 
    environment.account + environment.createRole, putRequestRoleModel).toPromise();

    return responseRoleModel;
  }

  public async deleteRole(name : string) : Promise<ResponseRoleModel>{
    const responseRoleModel : ResponseRoleModel = await this.http.delete<ResponseRoleModel>(
    environment.protocol + environment.host + environment.port + 
    environment.account + environment.deleteRole).toPromise();

    return responseRoleModel;
  }
}
