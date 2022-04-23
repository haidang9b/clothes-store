import { Component, Input, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ProductManagementService } from '../product-management/product-management.service';
import { AuthService } from '../../shared/auth.service';
import { MessageService } from 'primeng/api';
import { CartService } from '../../shared/cart.service';
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit {
  @Input() product: any;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productManagementService: ProductManagementService,
    private titleService: Title,
    private location: Location,
    private authService: AuthService,
    private messageService: MessageService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.getProductFromRoute();
  }

  getProductFromRoute() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productManagementService.getProduct(id).subscribe((res) => {
      this.product = res.data;
      if (this.product) {
        this.titleService.setTitle(`Detail Product - ${this.product.title}`);
      } else {
        this.titleService.setTitle(`Detail Product - Not Found`);
      }
    });
  }

  addToCart() {
    if (this.authService.loggedIn()) {
      console.log(this.authService.currentUser);
      if (this.authService.currentUser.role == 'Customer') {
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: `Product ${this.product.title} added to cart`,
        });
        this.cartService.addToCart(this.product);
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
