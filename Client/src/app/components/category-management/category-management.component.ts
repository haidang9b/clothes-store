import { Component, OnInit } from '@angular/core';
import { CategoryManagementService } from './category-management.service';
import { Category } from 'src/models/category';
import { MessageService } from 'primeng/api';
import { ConfirmDialogService } from '../confirm-dialog/confirm-dialog.service';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';
import { DialogCategoryComponent } from '../dialog-category/dialog-category.component';
import { AuthService } from '../../shared/auth.service';

@Component({
  selector: 'app-category-management',
  templateUrl: './category-management.component.html',
  styleUrls: ['./category-management.component.scss'],
})
export class CategoryManagementComponent implements OnInit {
  isOpenModal: boolean = false;
  categoryRemove: Category = new Category();
  categories: Category[] = [];
  constructor(
    private dialog: MatDialog,
    private categoryManagementService: CategoryManagementService,
    private messageService: MessageService,
    private confirmDialogService: ConfirmDialogService,
    private titleService: Title,
    public authService: AuthService
  ) {}
  getAllCategories(): void {
    this.categoryManagementService.getCategories().subscribe(
      (data) => {
        this.categories = data.data;
      },
      (error) => {
        this.categories = [];
      }
    );
  }
  RemoveCategory(obj: Category): void {
    this.confirmDialogService
      .confirmDialog({
        message: `Bạn có chắc muốn xóa danh mục ${obj.name}?`,
        title: 'Xác nhận xóa',
        confirmText: 'Yes',
        cancelText: 'No',
      })
      .subscribe((isConfirmed) => {
        if (isConfirmed) {
          this.categoryManagementService
            .removeCategory(obj.id)
            .subscribe((res) => {
              if (res.isSuccess) {
                this.messageService.add({
                  key: 'tr',
                  severity: 'success',
                  summary: 'Xóa thành công',
                  detail: `Xóa danh mục ${obj.name} thành công`,
                });
                this.categories = this.categories.filter(
                  (c) => c.id !== obj.id
                );
              } else {
                this.messageService.add({
                  key: 'tr',
                  severity: 'error',
                  summary: 'Xóa thất bại',
                  detail: `Xóa danh mục ${obj.name} thất bại`,
                });
              }
            });
        }
      });
  }

  EditCategory(obj: Category): void {
    let data = {
      requestType: 1,
      id: obj.id,
      name: obj.name,
    };
    const dialogRef = this.dialog.open(DialogCategoryComponent, {
      width: '60%',
      data,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result === null || result === undefined) {
        return;
      }
      let obj = {
        id: result.id,
        name: result.name,
      };
      this.categoryManagementService.updateCategory(obj).subscribe((res) => {
        if (res.isSuccess) {
          this.messageService.add({
            key: 'tr',
            severity: 'success',
            summary: 'Success',
            detail: res.message,
            life: 3000,
          });
          this.getAllCategories();
        } else {
          this.messageService.add({
            key: 'tr',
            severity: 'error',
            summary: 'Error',
            detail: res.message,
            life: 3000,
          });
        }
      });
    });
  }
  AddCategory(): void {
    let data = {
      requestType: 2,
      id: 0,
      name: '',
    };
    const dialogRef = this.dialog.open(DialogCategoryComponent, {
      width: '60%',
      data,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result === null || result === undefined) {
        return;
      }
      let obj = {
        id: result.id,
        name: result.name,
      };
      this.categoryManagementService.addCategory(obj).subscribe((res) => {
        if (res.isSuccess) {
          this.messageService.add({
            key: 'tr',
            severity: 'success',
            summary: 'Success',
            detail: res.message,
            life: 3000,
          });
          this.getAllCategories();
        } else {
          this.messageService.add({
            key: 'tr',
            severity: 'error',
            summary: 'Error',
            detail: res.message,
            life: 3000,
          });
        }
      });
    });
  }
  ngOnInit(): void {
    this.getAllCategories();
    this.titleService.setTitle('Category management');
  }
}
