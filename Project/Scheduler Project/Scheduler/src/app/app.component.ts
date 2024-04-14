import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { LoginComponent } from './login/login.component';
import { DataService } from './data.service';
import { Appointment, UserTable } from 'src/UserModel';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { SignupComponent } from './signup/signup.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Scheduler';
  currentUser: any;
  appointments: Appointment[] = [];
  constructor(private dialog: MatDialog, private data: DataService, private service: ApiService) { }
  
  public isLoggedIn = this.data.getLogginStatus();

  onloginclicked() {
    let dialogRef = this.dialog.open(LoginComponent);
    dialogRef.afterClosed().subscribe(res => {
      this.isLoggedIn = this.data.getLogginStatus();

      if(this.isLoggedIn){
        this.service.getUser().subscribe((user: UserTable)=>{
          this.currentUser = user;  
        });   
        
          this.service.getAppointments().subscribe((res: Appointment[]) => {
            this.appointments = res;
            console.log(this.appointments);
          })
      }
    })
  }

  onSignOutClicked() {
    localStorage["token"] = "";
    this.isLoggedIn = false;
    this.data.setLogginStatus(this.isLoggedIn);
    location.reload();
  }

  onAddClicked() {

  }

  onSignupClicked() {
    var dialogRef = this.dialog.open(SignupComponent);
  }
}