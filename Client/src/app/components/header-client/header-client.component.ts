import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/auth.service';
import { ConfirmDialogService } from '../confirm-dialog/confirm-dialog.service';
import { CategoryManagementService } from '../category-management/category-management.service';
import { Category } from '../../../models/category';
import { CartService } from '../../shared/cart.service';
@Component({
  selector: 'app-header-client',
  templateUrl: './header-client.component.html',
  styleUrls: ['./header-client.component.scss'],
})
export class HeaderClientComponent implements OnInit {
  totalCart: number = 0;
  categories: Category[] = [];
  user!: any;
  constructor(
    public authService: AuthService,
    public cartService: CartService,
    private confirmDialogService: ConfirmDialogService,
    private categoryManagementService: CategoryManagementService
  ) {
    if (authService.isLoggedIn$) {
      this.user = authService.currentUser;
    } else {
      this.user = null;
    }
  }
  Logout() {
    this.confirmDialogService
      .confirmDialog({
        message: `Are you sure you want to log out?`,
        title: 'Confirm Logout',
        confirmText: 'Yes',
        cancelText: 'No',
      })
      .subscribe((isConfirmed) => {
        if (isConfirmed) {
          this.authService.logout();
          this.user = null;
        }
      });
  }
  ngOnInit(): void {
    this.getCategories();
  }
  getCategories() {
    this.categoryManagementService.getCategories().subscribe((res) => {
      if (res.isSuccess) {
        this.categories = res.data;
      } else {
        this.categories = [];
      }
    });
  }
}
