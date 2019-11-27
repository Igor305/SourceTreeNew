import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PostRequestRegisterModel } from '../../models/request/post.request.register.model'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  postRequestRegisterModel: PostRequestRegisterModel = {};
  registrationMessage : string[];

  constructor(private authService : AuthService) { }

  async reg(){
    this.registrationMessage = await this.authService.registration(this.postRequestRegisterModel);
    console.log(this.registrationMessage);
    return this.registrationMessage;
  }
}