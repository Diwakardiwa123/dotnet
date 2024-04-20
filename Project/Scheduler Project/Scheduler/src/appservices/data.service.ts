import { Injectable } from '@angular/core';
import { Appointment } from 'src/UserModel';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  logginStatus = false;
  appointments: Appointment[] = [];
  constructor(private service: ApiService) { }

  getLogginStatus(): boolean{
    return this.logginStatus;
  }

  setLogginStatus = (login: boolean) => this.logginStatus = login;
}
