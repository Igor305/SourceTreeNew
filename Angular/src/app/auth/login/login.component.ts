import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PostRequestLoginModel } from '../../models/request/post.request.login.model';
import { PostResponseLoginModel } from '../../models/response/post.response.login.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  postRequestLoginModel : PostRequestLoginModel = {};
  postResponseLoginModel : PostResponseLoginModel = {};

  constructor(private authService : AuthService) { }

  loginForm : FormGroup = new FormGroup({

    email: new FormControl('', Validators.compose([
      Validators.required,
      Validators.email
    ])),
    password: new FormControl('', Validators.compose([
      Validators.required,
      Validators.minLength(8),
      Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]+$')
    ]))
  })

  private async login(){
    this.postResponseLoginModel = await this.authService.login(this.postRequestLoginModel);
    console.log(this.postRequestLoginModel);
  }
}