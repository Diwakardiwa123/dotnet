import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  logginStatus = false;
  constructor() { }

  getLogginStatus(): boolean{
    return this.logginStatus;
  }

  setLogginStatus = (login: boolean) => this.logginStatus = login;
}
