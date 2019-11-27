import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { PostRequestForgotPasswordModel } from 'src/app/models/request/post.request.forgotPassword.model';
import { BaseResponseModel } from 'src/app/models/response/base.response.model';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {
  postRequestForgotPasswordModel : PostRequestForgotPasswordModel = {};
  baseResponseModel : BaseResponseModel = {};

  constructor(private authService : AuthService) { }

  public async forgot (){
    this.baseResponseModel = await this.authService.forgotPassword(this.postRequestForgotPasswordModel);
    console.log(this.baseResponseModel.message);
  }   
}
