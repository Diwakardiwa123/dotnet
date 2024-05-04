import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserTable } from 'src/UserModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http : HttpClient) { }

  apiUrl = "http://192.168.29.214/api";

  getLoginStatus(): any{
    var url = this.apiUrl + "/User/GetLoginStatus"
    var header = this.getHeaders();

    return this.http.get(url, {headers: header});
  }

  postLogin(userLogin: any){
    var loginUrl = this.apiUrl+ "/Login";
    localStorage["token"] = "";
    var header = this.getHeaders();    
    const httpOptions = {
      headers : header
    }
    return this.http.post<any>(loginUrl, userLogin, httpOptions)
  }

  getUser(): Observable<any>{
    var url = this.apiUrl + "/user/GetUser";
    let header = this.getHeaders();
    
    return this.http.get<any>(url, {headers: header})
  }

  getAppointments(): Observable<any>{
    var url = this.apiUrl + "/Appointments";
    let header = this.getHeaders();

    return this.http.get<any>(url, {headers: header})
  }

  postUser(user: any){
    var url = this.apiUrl + "/User/PostUser";
    var header = this.getHeaders();
    return this.http.post<any>(url, user, {headers : header});
  }

  postAppointment(appointment : any){
    var url = this.apiUrl + "/Appointments/PostAppointment";
    var header = this.getHeaders();
    return this.http.post<any>(url, appointment, {headers : header});
  }

  deleteUser(user : any){
    var url = this.apiUrl + "/User/Remove";
    var header = this.getHeaders();
    return this.http.delete(url, {headers: header, body: user});
  }

  deleteAppointment(appointment: any){
    var url = this.apiUrl + "/Appointments/Remove";
    var header = this.getHeaders();
    return this.http.delete<any>(url, {headers: header, body: appointment});
  }

  updateUser(user : any){
  var url = this.apiUrl + "/User/Update";
  var header = this.getHeaders();
  return this.http.put(url, user, {headers: header});
  }

  updateAppointment(appointment: any){
    var url = this.apiUrl + "/Appointments/Update";
    var header = this.getHeaders();
    return this.http.put(url, appointment, {headers: header});
  }

  getHeaders(): HttpHeaders{
    let headerParams = {
      'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + localStorage["token"]
    }
    return new HttpHeaders(headerParams);
  }

}
