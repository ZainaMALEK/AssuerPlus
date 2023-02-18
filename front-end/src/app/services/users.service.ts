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

  private baseUrl = '/api/Users';
  constructor(private httpClient: HttpClient) {}

  login(form: any) {
    return false;
  }
}
