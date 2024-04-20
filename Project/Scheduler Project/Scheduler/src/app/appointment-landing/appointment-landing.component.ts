import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Appointment, UserTable } from 'src/UserModel';
import { DataService } from 'src/appservices/data.service';
import { AppComponent } from '../app.component';
import { ApiService } from 'src/appservices/api.service';
import { catchError, map, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-appointment-landing',
  templateUrl: './appointment-landing.component.html',
  styleUrls: ['./appointment-landing.component.css']
})
export class AppointmentLandingComponent {

  appointments: Appointment[] = [];

  constructor(private service: ApiService, private route: ActivatedRoute)
  { 
  }

  ngOnInit(){
      try{
        this.service.getAppointments()
        .pipe( map((val : Appointment[]) => this.appointments = val) )
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
          return date > new Date();
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
}