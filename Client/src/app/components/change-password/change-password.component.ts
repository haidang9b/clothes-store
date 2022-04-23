import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../shared/auth.service';
import { MessageService } from 'primeng/api';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  checkPasswords: ValidatorFn = (
    group: AbstractControl
  ): ValidationErrors | null => {
    let pass = group.get('passwordNew')?.value;
    let confirmPass = group.get('passwordNewConfirm')?.value;
    return pass === confirmPass ? null : { notSame: true };
  };
  
  formGroup: FormGroup = new FormGroup({
    passwordOld: new FormControl({value: '', disabled: false},[Validators.required, Validators.minLength(6)]),
    passwordNew: new FormControl({value: '', disabled: false},[Validators.required, Validators.minLength(6)]),
    passwordNewConfirm: new FormControl({value: '', disabled: false},[Validators.required, Validators.minLength(6)])
  }, this.checkPasswords)
  constructor(
    private authService: AuthService,
    private messageService: MessageService,
    private titleService: Title
  ) {
    this.titleService.setTitle('Change Password');
  }

  ngOnInit(): void {
  }

  onChangePassword(){
    if(this.formGroup.valid === false){
      this.messageService.add({key: 'tr',severity:'error', summary:'Error', detail:'Please fill all fields & valid', life:5000});
      return;
    }
    var obj = {
      passwordOld: this.formGroup.controls['passwordOld'].value,
      passwordNew: this.formGroup.controls['passwordNewConfirm'].value
    }
    this.authService.changePassword(obj).subscribe(res => {
      if(res.isSuccess){
        this.messageService.add({key:'tr',severity:'success', summary:'Success', detail:'Change password success', life:5000});
        this.formGroup.reset();
      }
      else{
        this.messageService.add({key:'tr',severity:'error', summary:'Error', detail:res.message, life:5000});
      }
    })
  }
}
