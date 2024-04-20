import { Component, OnInit, OnChanges } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { DataService } from 'src/appservices/data.service';
import { Appointment, UserTable } from 'src/UserModel';
import { Observable } from 'rxjs';
import { ApiService } from 'src/appservices/api.service';
import { SignupComponent } from '../signup/signup.component';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  appointments: Appointment[] = [];
  constructor(private dialog: MatDialog, private data: DataService, private service: ApiService, private router: Router) { }
  
  public isLoggedIn = this.data.getLogginStatus();

  ngOnInit(){
    console.log(this.isLoggedIn);
    this.service.getLoginStatus().subscribe((res: any)=>{
          this.isLoggedIn = res;
          if(this.isLoggedIn) this.router.navigateByUrl("/home/appointment");
        }); 
  }

  onloginclicked() {
    let dialogRef = this.dialog.open(LoginComponent);
    dialogRef.afterClosed().subscribe(res => {
      this.isLoggedIn = this.data.getLogginStatus();

      if(this.isLoggedIn){        
          this.service.getAppointments().subscribe((res: Appointment[]) => {
            this.appointments = res;
          })
      }
    })
  }

  onSignOutClicked() {
    localStorage["token"] = "";
    this.isLoggedIn = false;
    this.data.setLogginStatus(this.isLoggedIn);
    this.router.navigateByUrl("/home");
  }

  onAddClicked() {
    this.router.navigateByUrl("/home/add");
  }

  onSignupClicked() {
    var dialogRef = this.dialog.open(SignupComponent);
  }

}
