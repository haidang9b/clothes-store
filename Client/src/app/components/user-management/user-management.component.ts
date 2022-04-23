import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/auth.service';
import { MessageService } from 'primeng/api';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss'],
})
export class UserManagementComponent implements OnInit {
  users: any[] = [];
  roles: any[] = [];
  constructor(
    private authService: AuthService,
    private messageService: MessageService,
    private titleService: Title
  ) {
    this.titleService.setTitle('User Management');
  }

  ngOnInit(): void {
    this.authService.getRoles().subscribe((res) => {
      if (res.isSuccess) {
        this.roles = res.data;
      } else {
        this.roles = [];
      }
    });
    this.authService.getUsers().subscribe((res) => {
      if (res.isSuccess) {
        this.users = res.data;
      } else {
        this.users = [];
      }
    });
  }

  onChangeRole(role_id: any, user: any) {
    var obj = {
      user_id: user.id,
      role_id: role_id,
    };
    this.authService.changePermission(obj).subscribe((res) => {
      if (res.isSuccess) {
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: `${user.username} - ${res.message}`,
        });
      } else {
        this.messageService.add({
          key: 'tr',
          severity: 'error',
          summary: 'Error',
          detail: `${user.username} - ${res.message}`,
        });
      }
    });
  }
}
