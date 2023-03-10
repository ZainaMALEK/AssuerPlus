import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})

export class UsersService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    }),
  };

  private baseUrl = 'http://localhost:11940/api/';
  constructor(private httpClient: HttpClient) {}

  login(form: any) {
    return this.httpClient.post<any>(this.baseUrl + "login", form, this.httpOptions);

  }
  logout(){
    localStorage.removeItem('jwt');
  }

   getUserInfo() {
    const token = this.getToken();
    let payload;
    if (token) {
      payload = token.split(".")[1];
      payload = window.atob(payload);
      console.log(payload);

      return JSON.parse(payload);
    } else {
      return null;
    }
  }

   getToken() {
    return localStorage.getItem("jwt-token");
  }
}
