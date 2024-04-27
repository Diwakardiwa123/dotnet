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
  selector: 'app-appointment-landing',
  templateUrl: './appointment-landing.component.html',
  styleUrls: ['./appointment-landing.component.css']
})
export class AppointmentLandingComponent {
  
}