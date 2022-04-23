import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../shared/auth.service';
import { MessageService } from 'primeng/api';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginFormGroup!: FormGroup;
  constructor(
    private authService: AuthService,
    private messageService: MessageService,
    private router: Router,
    private titleService: Title
  ) {
    if (this.authService.loggedIn()) {
      this.router.navigate(['/']);
      return;
    }
    this.titleService.setTitle('Clothes Shop - Login');
  }
  ngOnInit(): void {
    this.loginFormGroup = new FormGroup({
      username: new FormControl({ value: '', disabled: false }, [
        Validators.required,
        Validators.minLength(3),
      ]),
      password: new FormControl({ value: '', disabled: false }, [
        Validators.required,
        Validators.minLength(5),
      ]),
    });
  }

  onSubmit() {
    if (this.loginFormGroup.valid === false) {
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Please fill all required fields',
      });
      return;
    }
    let obj = {
      username: this.loginFormGroup.controls['username'].value,
      password: this.loginFormGroup.controls['password'].value,
    };
    this.authService.login(obj).subscribe((response) => {
      if (response.isSuccess) {
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: 'Login Success',
        });
        this.loginFormGroup.reset();
        this.router.navigate(['/']);
      } else {
        this.messageService.add({
          key: 'tr',
          severity: 'error',
          summary: 'Login Error',
          detail: response.message,
        });
      }
    });
  }
}
