import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  baseURL: string = "https://htla2yapi.azurewebsites.net/Account/Register";
  UsersURL: string = "https://htla2yapi.azurewebsites.net/api/users";

  constructor(private httpClient: HttpClient) { }


  validateUsername(username) {
    return this.httpClient.get(`${this.UsersURL}/${username}`);
  }

  RegisterUser(data: Object): Observable<Object> {
    return this.httpClient.post(this.baseURL, data);
  }
}


