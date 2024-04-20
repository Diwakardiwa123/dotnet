import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/appservices/api.service';
import { DataService } from 'src/appservices/data.service';

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

  constructor(private apiservie: ApiService, private dataservice: DataService, private dialogRef: MatDialogRef<SignupComponent>){}

  onSignupClicked() {
    const user = {
      'userName' : this.userName,
      'userPassword' : this.userPassword,
      'userAddress' : this.address,
      'mobileNumber' : this.mobile,
      'email' : this.email
    };

    this.apiservie.postUser(user).subscribe((res) => {
      this.dialogRef.close();
    })
  }

}
