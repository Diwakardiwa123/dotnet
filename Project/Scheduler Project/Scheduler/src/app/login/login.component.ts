import { Component, EventEmitter, Output } from '@angular/core';
import { ApiService } from 'src/appservices/api.service';
import { DataService } from 'src/appservices/data.service';

import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { Observable, catchError, map } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
    userName = "";
    password = "";
    isLoggedIn = false;
    errorDetail = "";
  
  constructor(private service: ApiService, private dialogRef: MatDialogRef<LoginComponent>, private data: DataService){}

  onLoginClick() {
    
    const userLogin = {
      'userName' : this.userName,
      'password' : this.password
    }

    this.service.postLogin(userLogin)
      .pipe(
        map((val : any) => {
        localStorage["token"] = val +'';
        this.data.setLogginStatus(true);
        this.dialogRef.close();
        location.reload();
        }),
        catchError((error :HttpErrorResponse) => {
          if (error.status == 400) this.errorDetail = error.error;
          return Observable.name; // Don't what I've done here. But the code worked.
        })
    )
    .subscribe();
  }
}
