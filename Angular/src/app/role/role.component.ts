import { Component, OnInit } from '@angular/core';
import { RoleService } from '../services/role.service';
import { PostCreateRequestRoleModel } from '../models/request/postCreate.request.role.model';
import { PostCreateRequestRoleUserModel } from '../models/request/postCreate.request.roleUser.model';
import { PutRequestRoleModel } from '../models/request/put.request.role.model';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {
  postCreateRequestRoleModel : PostCreateRequestRoleModel = {};
  postCreateRequestRoleUserModel : PostCreateRequestRoleUserModel = {};
  putRequestRoleUserModel : PutRequestRoleModel = {};
  roleName : string = '';
  constructor(private roleService : RoleService) { }

  public async getAll(){
    await this.roleService.getAllRoles();
  }
  public async postCreate(){
    await this.roleService.postCreateRole(this.postCreateRequestRoleModel);
  }
  public async postAddingRoleUser(){
    await this.roleService.postAddingRoleUser(this.postCreateRequestRoleUserModel);
  }
  public async postTakingAwayRoleUser(){
    await this.roleService.postTakingAwayRoleUser(this.postCreateRequestRoleUserModel);
  }
  public async putUpdate(){
    await this.roleService.putUpdateRole(this.putRequestRoleUserModel, this.roleName);
  }
  public async delete(){
    await this.roleService.deleteRole(this.roleName);
  }
  ngOnInit() {
  }

}
