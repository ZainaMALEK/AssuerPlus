import { UsersService } from '../../services/users.service';
import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-declare-accident',
  templateUrl: './declare-accident.component.html',
  styleUrls: ['./declare-accident.component.scss'],
})

export class DeclareAccidentComponent {
  images!: File[];

  description: string = '';
  url: string = 'http://localhost:11940/api/declareAccident';
  validated = false;
  user: any;
  userjson: any = '';

  /* form = new FormGroup({
    description: new FormControl('', Validators.required),
  }); */

  form = new FormGroup({
    description: new FormControl('', Validators.required),
    images: new FormControl(null, this.validateImages)
  });
  validateImages(control: AbstractControl | null): { [key: string]: boolean } | null {
    const files = control!.value as File[];
    if (!files || files.length === 0) {
      return { 'noImages': true };
    }
    return null;
  }
  constructor(
    private httpClient: HttpClient,
    private usersService: UsersService
  ) {
    this.userjson = localStorage.getItem('user');
    this.user = JSON.parse(this.userjson);
  }
  sendData() {
    console.log(this.form.value);

    const formData = new FormData();
    this.description = <string>this.form.value.description;
    formData.append('description', this.description);

    Array.from(this.images).forEach((file) => {
      formData.append('images', file);
    });

    formData.append('clientID', this.user.ClientID);

    this.httpClient.post(this.url, formData, {responseType: 'text'}).subscribe((response) => {
      console.log(response);
      if(response == "success"){
        this.validated = true;
      }
    });
  }

  onFileSelected(event: any) {
    console.log(this.usersService.getUserInfo());

    const files: File[] = event.target.files;
    this.images = files;

  }

  atLeastOneImage(control: FormControl) {
    const files: File[] = control.value;
    if (!files || files.length === 0) {
      return { noImageSelected: true };
    }
    return null;
  }
}
