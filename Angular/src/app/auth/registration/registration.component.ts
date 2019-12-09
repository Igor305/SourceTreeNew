import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PostRequestRegisterModel } from '../../models/request/post.request.register.model'; 
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent {
  postRequestRegisterModel: PostRequestRegisterModel = {};
  registrationMessage : string[];

  constructor(private authService : AuthService) { }

  registrationForm : FormGroup = new FormGroup({  
    email: new FormControl('', Validators.compose([ 
      Validators.required, 
      Validators.email
    ])),
    password: new FormControl('', Validators.compose([
      Validators.required, 
      Validators.minLength(8),
      Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]+$')
    ])),
    passwordConfirm: new FormControl('', Validators.required)
  }, {validators: this.checkPassword});

  private checkPassword(group: FormGroup) {
    let password = group.get('password').value;
    let passwordConfirm = group.get('passwordConfirm').value;

    return password === passwordConfirm ? null : { notSame: true }     
  }

  private async registration(){
    this.registrationMessage = await this.authService.registration(this.postRequestRegisterModel);
    console.log(this.registrationMessage);
  }
}