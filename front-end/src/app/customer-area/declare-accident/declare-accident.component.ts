import { UsersService } from '../../services/users.service';
import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

class SinistreDTO {
  constructor(
    public description: string = '',
    public images: ImageDTO[] = []
  ) {}
}

class ImageDTO {
  constructor(
    public base64: string = '',
    public contentType: string = ''
  ) {}
}

@Component({
  selector: 'app-declare-accident',
  templateUrl: './declare-accident.component.html',
  styleUrls: ['./declare-accident.component.scss'],
})



export class DeclareAccidentComponent {
  images!: File[];

  description: string = '';
  url: string = 'http://localhost:11940/api/declareAccident';
  //url: string = ' http://vps-506fba80.vps.ovh.net:8081/api/declareAccident';
  validated = false;
  user: any;
  userjson: any = '';
  userSinistres: SinistreDTO[] = [];


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
    console.log(this.user.ClientID);

    this.getUserSinistres();
  }
  sendData() {
    console.log(this.form.value);

    const formData = new FormData();
    this.description = <string>this.form.value.description;
    formData.append('description', this.description);
    let imagesDto: ImageDTO[] = [];
    Array.from(this.images).forEach((file) => {

      formData.append('images', file);

      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        if(reader.result !=null){

          const base64String = reader.result.toString().split(',')[1];
          const extension = file.name.split('.').pop();
          if (extension) {
            let imageDto  = new ImageDTO(base64String, extension);
            imagesDto.push(imageDto);
          }

        }
      };

    });

    formData.append('clientID', this.user.ClientID);

    this.httpClient.post(this.url, formData, {responseType: 'text'}).subscribe((response) => {
      console.log(response);
      this.validated = true;
      let s = new SinistreDTO();
      s.description = this.description;
      s.images = imagesDto;
      this.userSinistres.unshift(s);


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

  getUserSinistres(){
    this.usersService.getUserSinistres(this.user.ClientID ).subscribe(res => {
      console.log(res);

      this.userSinistres = res;


    });
  }
}
