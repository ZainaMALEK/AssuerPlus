import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

interface SinistreDTO {
  sinistreID:number,
  description: string,
  images: ImageDTO[]

}

interface ImageDTO {

  base64: string,
  contentType: string

}


@Injectable({
  providedIn: 'root',
})

export class UsersService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    }),
  };
  private baseUrl = 'http://vps-506fba80.vps.ovh.net:8081/api/';

  //private baseUrl = 'http://localhost:11940/api/';
  constructor(private httpClient: HttpClient) {}

  login(form: any) {
    return this.httpClient.post<any>(this.baseUrl + "login", form, this.httpOptions);

  }
  logout(){
    localStorage.removeItem('jwt');
  }

   getUserInfo() {
    const token = this.getToken();
    console.log(token);

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
    return localStorage.getItem("jwt");
  }
  getUserSinistres(clientId :any){
    const params = new HttpParams().set('userId', clientId);

    return this.httpClient.get<SinistreDTO[]>(this.baseUrl + 'userSinistres', { params: params });
  }
}
