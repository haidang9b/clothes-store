import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../shared/auth.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  checkPasswords: ValidatorFn = (
    group: AbstractControl
  ): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('confirmPassword')?.value;
    return pass === confirmPass ? null : { notSame: true };
  };

  registerFormGroup: FormGroup = new FormGroup(
    {
      username: new FormControl({ value: '', disabled: false }, [
        Validators.required,
        Validators.minLength(3),
      ]),
      password: new FormControl({ value: '', disabled: false }, [
        Validators.required,
        Validators.minLength(5),
      ]),
      confirmPassword: new FormControl({ value: '', disabled: false }, [
        Validators.required,
        Validators.minLength(5),
      ]),
      fullName: new FormControl({ value: '', disabled: false }, [
        Validators.required,
        Validators.minLength(2),
      ]),
    },
    this.checkPasswords
  );
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
    this.titleService.setTitle('Clothes Shop - Register');
  }

  onRegister(): void {
    if (this.registerFormGroup.valid === false) {
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Please fill all required fields',
      });
      return;
    }

    let obj = {
      username: this.registerFormGroup.controls['username'].value,
      password: this.registerFormGroup.controls['password'].value,
      fullName: this.registerFormGroup.controls['fullName'].value,
    };
    this.authService.register(obj).subscribe((response) => {
      if (response.isSuccess) {
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: 'Register Success & You can login now',
        });
        this.registerFormGroup.reset();
      } else {
        this.messageService.add({
          key: 'tr',
          severity: 'error',
          summary: 'Register Error',
          detail: response.message,
        });
      }
    });
  }

  ngOnInit(): void {}
}
