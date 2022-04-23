import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { Category } from 'src/models/category';
import { Product } from 'src/models/product';
import { DialogProductComponent } from '../dialog-product/dialog-product.component';
import { ProductManagementService } from './product-management.service';
import { ConfirmDialogService } from '../confirm-dialog/confirm-dialog.service';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../shared/auth.service';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.scss']
})
export class ProductManagementComponent implements OnInit {
  products: Product[] = [];
  constructor(
    private titleService: Title,
    private dialog: MatDialog,
    private productManagementService: ProductManagementService,
    private confirmDialogService: ConfirmDialogService,
    private messageService: MessageService,
    public authService: AuthService
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle("Product Management");
    this.getProducts();
  }

  DeleteProduct(obj: Product){
    this.confirmDialogService.confirmDialog({
      message: `Bạn có chắc muốn xóa sản phẩm ${obj.title}?`,
      title: 'Xác nhận xóa',
      confirmText: "Yes",
      cancelText: "No"
    }).subscribe(isConfirmed => {
      if (isConfirmed) {
        this.productManagementService.removeProduct(obj.id).subscribe(res => {
          if(res.isSuccess) {
            this.messageService.add({
              key: 'tr',
              severity: 'success',
              summary: 'Xóa thành công',
              detail: `Xóa sản phẩm ${obj.title} thành công`
            });
            this.products = this.products.filter(c => c.id !== obj.id);
          }
          else{
            this.messageService.add({
              key: 'tr',
              severity: 'error',
              summary: 'Xóa thất bại',
              detail: `Xóa sản phẩm ${obj.title} thất bại`
            });
          }
        })
      }
    })
  }
  EditProduct(product: Product){
    var data = {
      requestType: 1,
      title: product.title,
      description: product.description,
      price: product.price,
      quantity: product.quantity,
      image: product.image,
      category: {
        id: product.category.id,
        name: product.category.name
      }
    }
    this.dialog.open(DialogProductComponent,{
      width: "60%",
      data,
      disableClose: true
    }).afterClosed().subscribe(result => {
      if(result !== undefined && result !== null){
        let obj = {
          id: product.id,
          title: result.title,
          description: result.description,
          price: result.price,
          quantity: result.quantity,
          image: result.image,
          category: result.category['value'] | result.category
        }
        console.log(obj);
        this.productManagementService.updateProduct(obj).subscribe(result => {
          if(result.isSuccess){
            this.messageService.add({
              key: 'tr',
              severity: 'success',
              summary: 'Success',
              detail: result.message,
              life:3000
            });
            this.getProducts();
          }
          else{
            this.messageService.add({
              key: 'tr',
              severity: 'error',
              summary: 'Error',
              detail: result.message,
              life:3000
            });
          }
          
        })
      }
    })

  }
  
  getProducts() {
    this.productManagementService.getProducts().subscribe(
      (res) => {
        if(res.isSuccess){
          this.products = res.data;
        }
      },
      (error) => {
        this.products = [];
        console.log(error);
      }
    );
  }
  AddProduct(){
    var data = {
      requestType: 2,
      title: "",
      description: "",
      price: 0,
      quantity: 0,
      image: "",
      category: {
        id: 1,
        name: ""
      }
    }
    this.dialog.open(DialogProductComponent,{
      width: "60%",
      data,
      disableClose: true
    }).afterClosed().subscribe(result => {
      if(result !== undefined && result !== null){
        let obj = {
          title: result.title,
          description: result.description,
          price: result.price,
          quantity: result.quantity,
          image: result.image,
          category: result.category['value'] | result.category
        }
        console.log(obj);
        this.productManagementService.addProduct(obj).subscribe(result => {
          console.log(result);
          if(result.isSuccess){
            this.messageService.add({
              key: 'tr',
              severity: 'success',
              summary: 'Success',
              detail: result.message,
              life:3000
            });
            this.getProducts();
          }
          else{
            this.messageService.add({
              key: 'tr',
              severity: 'error',
              summary: 'Error',
              detail: result.message,
              life:3000
            });
          }
          
        })
      }
    })

  }
}
