import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { LoginComponent } from './login/login.component';
import { DataService } from 'src/appservices/data.service';
import { Appointment, UserTable } from 'src/UserModel';
import { Observable } from 'rxjs';
import { ApiService } from 'src/appservices/api.service';
import { SignupComponent } from './signup/signup.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Scheduler';
  
}