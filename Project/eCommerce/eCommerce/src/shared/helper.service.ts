import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import  UserModel  from '../shared/UserModel'
import { Observable } from 'rxjs';

@Injectable()
export default class HelperService {
    public homeController = 'https://localhost:44364/api/UserProfile/GetUsers';
    public apiController = 'https://localhost:44364/api/UserProfile/UserPost';

    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };


    constructor(private http: HttpClient) { }

    OnPostUser(): Observable<Array<UserModel>> {

        return this.http.get<Array<UserModel>>(this.homeController, this.httpOptions);
    }

    post(user: UserModel) {
        this.http.post<UserModel>(this.apiController, user, this.httpOptions).subscribe();
    }
}