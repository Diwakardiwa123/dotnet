import { Component } from '@angular/core';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  userName = "";
  userPassword = "";
  address = "";
  mobile = "";
  email = "";

  onSignupClicked() {
    
  }

}