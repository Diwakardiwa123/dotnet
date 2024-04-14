import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserTable } from 'src/UserModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http : HttpClient) { }

  apiUrl = "https://localhost:44352/api";

  postLogin(userLogin: any){
    var loginUrl = this.apiUrl+ "/Login";
    var header = this.getHeaders();
    return this.http.post<any>(loginUrl, userLogin, {headers: header})
  }

  getUser(): Observable<any>{
    var url = this.apiUrl + "/User/GetUser";
    let header = this.getHeaders();
    
    return this.http.get<any>(url, {headers: header})
  }

  getAppointments(): Observable<any>{
    var url = this.apiUrl + "/Appointments";
    let header = this.getHeaders();

    return this.http.get<any>(url, {headers: header})
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
