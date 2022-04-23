import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared/auth.service';
import { User } from 'src/models/user';
import { ConfirmDialogService } from '../confirm-dialog/confirm-dialog.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  user!: any;

  constructor(
    public authService: AuthService, private confirmDialogService: ConfirmDialogService
  ) { 
    if(authService.isLoggedIn$){
      this.user = authService.currentUser;
    }
    else{
      this.user = null;
    }
  }

  Logout(){
    this.confirmDialogService.confirmDialog({
      message: `Are you sure you want to log out?`,
      title: 'Confirm Logout',
      confirmText: "Yes",
      cancelText: "No"
    }).subscribe(isConfirmed => {
      if (isConfirmed) {
        this.authService.logout();
        this.user = null;
      }
    })
  }
  ngOnInit(): void {
  }

}
