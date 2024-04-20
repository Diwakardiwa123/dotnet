import { Injectable } from '@angular/core';
import base64url from 'base64url';
import { ApiService } from './api.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private apiservice : ApiService) { }
}
