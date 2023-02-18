import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { UsersService } from '../../services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-authentication-page',
  templateUrl: './authentication-page.component.html',
  styleUrls: ['./authentication-page.component.scss'],
})
export class AuthenticationPageComponent {
  form = new FormGroup({
    assuranceNumber: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  get assuranceNumber() {
    return this.form.get('assuranceNumber');
  }
  get password() {
    return this.form.get('password');
  }
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private usersService: UsersService
  ) {}

  ngOnInit() {}

  login() {
    let isValid = this.usersService.login(this.form.value);
    if (!isValid) {
      this.form.setErrors({
        invalidLogin: true,
      });
    }
  }
}
