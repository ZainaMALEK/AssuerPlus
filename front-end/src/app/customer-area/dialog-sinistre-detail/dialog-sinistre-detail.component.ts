
import { Component, Inject  } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

class SinistreDTO {
  constructor(
    public sinistreID: number = 0,
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
  selector: 'app-dialog-sinistre-detail',
  templateUrl: './dialog-sinistre-detail.component.html',
  styleUrls: ['./dialog-sinistre-detail.component.scss']
})
export class DialogSinistreDetailComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public sinistre: SinistreDTO) {


  }
}
