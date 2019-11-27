import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { PostCreateRequestUserModel } from '../models/request/postCreate.request.user.model';
import { ResponseUserModel } from '../models/response/response.user.model';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  responseUserModel: ResponseUserModel = {};
  postCreateRequestUserModel: PostCreateRequestUserModel = {}
  userId : string = '';

  constructor(private userService: UserService) { }

  async  getAll() {
    this.responseUserModel = await this.userService.getAll();

    this.responseUserModel = await this.userService.getAllWithoutRemove();
  }

  async create() {
    this.responseUserModel = await this.userService.postCreate(this.postCreateRequestUserModel);
  }

  async update(){
    this.responseUserModel = await this.userService.putUpdate(this.postCreateRequestUserModel, this.userId);
  }

  async deleteMarkAsDeleted(){
    this.responseUserModel = await this.userService.deleteMarkAsDeleted(this.userId);
  }

  async deleteFinalRemoval(){
    this.responseUserModel = await this.userService.deleteFinalRemoval(this.userId);
  }

  ngOnInit() {
    this.getAll();
  }

}
