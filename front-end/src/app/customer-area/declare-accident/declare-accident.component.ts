import { UsersService } from '../../services/users.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-declare-accident',
  templateUrl: './declare-accident.component.html',
  styleUrls: ['./declare-accident.component.scss']
})
export class DeclareAccidentComponent {
  images!: File[];
  //images!:File;
  description:string ="";
  url:string ="http://localhost:11940/api/declareAccident";
  form = new FormGroup({
    //images: new FormControl(null),
    description: new FormControl('dd'),
  });
  constructor(private httpClient: HttpClient, private usersService :UsersService) {

  }
  sendData() {
    console.log(this.form.value);

    const formData = new FormData();
    this.description = <string>this.form.value.description;
    formData.append('description', this.description)


    Array.from(this.images).forEach(file => {
      formData.append("images", file);

    });



    this.httpClient.post(this.url, formData).subscribe((response) => {
                console.log(response);
            });
  }

  onFileSelected(event:any) {
console.log(this.usersService.getUserInfo());

    //const files:File [] = event.target.files;
    const files:File[] = event.target.files;
    this.images = files;

    if (files) {
        /* console.log(files);
        const formData = new FormData();

        Array.from(files).forEach(file => {
          formData.append("thumbnail", file);

        }); */



    }
}
}
