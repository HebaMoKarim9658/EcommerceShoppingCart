import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EditProfileService {


  baseURL = "https://htla2yapi.azurewebsites.net";

  constructor(private myClient: HttpClient) { }

  EditUser(id, user) {
    console.log("Entered ");
    return this.myClient.put(`${this.baseURL}/api/Users/${id}`, user);

  }

}
