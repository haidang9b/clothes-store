import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/shared/auth.service';
import { CartService } from 'src/app/shared/cart.service';
import { Product } from '../../../models/product';
import { ProductManagementService } from '../product-management/product-management.service';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  arrivals: Product[] = [];
  getNewArrivals() {
    this.productManagementService.getNewArrivals(8).subscribe((res) => {
      if (res.isSuccess) {
        this.arrivals = res.data;
      } else {
        this.arrivals = [];
      }
    });
  }
  constructor(
    private cartService: CartService,
    private messageService: MessageService,
    private authService: AuthService,
    private productManagementService: ProductManagementService,
    private titleService: Title,
  ) {
    this.titleService.setTitle('Clothes Store - Home');
  }

  ngOnInit(): void {
    this.getNewArrivals();
  }
  addToCart(product: any) {
    if (this.authService.loggedIn()) {
      if (this.authService.currentUser.role == 'Customer') {
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: `Product ${product.title} added to cart`,
        });
        this.cartService.addToCart(product);
      } else {
        this.messageService.add({
          key: 'tr',
          severity: 'error',
          summary: 'Error',
          detail: 'You are not allowed to add to cart, please login as a user',
        });
      }
    } else {
      this.messageService.add({
        key: 'tr',
        severity: 'warn',
        summary: 'Please login to add to cart',
        detail: 'Please login to add this product to cart',
      });
    }
  }
}
