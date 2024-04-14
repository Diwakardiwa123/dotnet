import { Component, EventEmitter, Output } from '@angular/core';
import { ApiService } from '../api.service';
import { DataService } from '../data.service';

import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
    userName = "";
    password = "";
    isLoggedIn = false;
  
  constructor(private service: ApiService, private dialogRef: MatDialogRef<LoginComponent>, private data: DataService){}

  onLoginClick() {
    console.log(this.userName + this.password);

    const userLogin = {
      'userName' : this.userName,
      'password' : this.password
    }

    this.service.postLogin(userLogin).subscribe((res: any) => {
      if(res != null){
        localStorage["token"] = res +'';
        console.log(localStorage["token"]);
        this.data.setLogginStatus(true);
        this.dialogRef.close();
      }
    })
  }

}