import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { PostRequestForgotPasswordModel } from 'src/app/models/request/post.request.forgotPassword.model';
import { BaseResponseModel } from 'src/app/models/response/base.response.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {
  postRequestForgotPasswordModel : PostRequestForgotPasswordModel = {};
  baseResponseModel : BaseResponseModel = {};

  constructor(private authService : AuthService) { }

  forgotPasswordForm : FormGroup = new FormGroup({

    email: new FormControl('', Validators.compose([ 
      Validators.required, 
      Validators.email
    ])),
    password: new FormControl('', Validators.compose([
      Validators.required, 
      Validators.minLength(8),
      Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]+$')
    ])),
    confirmNewPassword: new FormControl('', Validators.required)
  }, {validators: this.checkPassword});

  private checkPassword(group: FormGroup) {
    let password = group.get('password').value;
    let confirmPassword = group.get('confirmNewPassword').value;

    return password === confirmPassword ? null : { notSame: true }     
  }

  private async forgot (){
    this.baseResponseModel = await this.authService.forgotPassword(this.postRequestForgotPasswordModel);
    console.log(this.baseResponseModel.message);
  }   
}
