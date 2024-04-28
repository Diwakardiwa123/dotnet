import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Appointment, UserTable } from 'src/UserModel';
import { DataService } from 'src/appservices/data.service';
import { AppComponent } from '../app.component';
import { ApiService } from 'src/appservices/api.service';
import { catchError, map, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DetailComponent } from '../detail/detail.component';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent {
  appointments: Appointment[] = [];

  constructor(private service: ApiService, private route: Router, private dialog: MatDialog)
  { 
  }

  ngOnInit(){
      try{
        this.service.getAppointments()
        .pipe( map((val : Appointment[]) => {
        this.appointments = val;
        console.log(this.appointments);
        }) )
        .subscribe({ error(err) { } });
      }
      catch (err : any){
      }     
      
    }

  ngOnDestroy(){
    this.appointments = [];
  }

  onUpcomingClicked() {
    if(this.appointments != null){
      this.appointments = this.appointments.filter((item : Appointment) => {
          let date = new Date(item.appointmentDate ?? "");
          return date >= new Date();
      });
    }
  }

  onPastClicked() {
    if(this.appointments != null){
      this.appointments = this.appointments.filter((item : Appointment) => {
          let date = new Date(item.appointmentDate ?? "");
          return date < new Date();
      });
    }
  }

  onAllClicked() {
    if(this.appointments != null){

      this.service.getAppointments()
        .pipe( map((val : Appointment[]) => this.appointments = val) )
        .subscribe({ error(err) { } });
    }
  }

  onDetailClicked(appointment: Appointment) {
    this.route.navigateByUrl("/home/appointment/detail");
  }
}
