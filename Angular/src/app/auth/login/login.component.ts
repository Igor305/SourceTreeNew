import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PostRequestLoginModel } from '../../models/request/post.request.login.model';
import { PostResponseLoginModel } from '../../models/response/post.response.login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  {
  postRequestLoginModel : PostRequestLoginModel = {};
  postResponseLoginModel : PostResponseLoginModel = {};

  constructor(private authService : AuthService) { }

  async login(){
    this.postResponseLoginModel = await this.authService.login(this.postRequestLoginModel);
  }
}