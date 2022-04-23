import { Component, Input, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductManagementService } from '../product-management/product-management.service';
import { CategoryManagementService } from '../category-management/category-management.service';
import { CartService } from '../../shared/cart.service';
import { MessageService } from 'primeng/api';
import { AuthService } from '../../shared/auth.service';
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  @Input() products: any[] = [];
  titleThisPage = '';
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productManagementService: ProductManagementService,
    private categoryManagementService: CategoryManagementService,
    private titleService: Title,
    private cartService: CartService,
    private messageService: MessageService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getProductsByUrl();
  }

  private getProducts(type: any) {
    this.productManagementService
      .getProductsByCategory(type)
      .subscribe((res) => {
        if (res.isSuccess) {
          this.products = res.data;
          this.products.reverse();
        } else {
          this.products = [];
        }
      });
  }

  addToCart(product: any) {
    if (this.authService.loggedIn()) {
      console.log(this.authService.currentUser);
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

  getProductsByUrl() {
    this.route.queryParams.subscribe((params) => {
      if (params['type'] && isNaN(params['type']) == false) {
        let type = params['type'];
        this.categoryManagementService
          .getCategoryByID(type)
          .subscribe((res) => {
            if (res.isSuccess) {
              this.titleService.setTitle(
                `Products by category - ${res.data.name}`
              );
              this.titleThisPage = `List ${res.data.name}`;
              this.getProducts(type);
            } else {
              this.titleThisPage = ``;
              this.titleService.setTitle(`Products - Not Found`);
            }
          });
      } else {
        this.productManagementService.getProducts().subscribe((res) => {
          if (res.isSuccess) {
            this.products = res.data;
            this.products.reverse();
            this.titleThisPage = `List All Products`;
            this.titleService.setTitle(`All - Products`);
          } else {
            this.products = [];
            this.titleThisPage = ``;
            this.titleService.setTitle(`Products - Not Found`);
          }
        });
      }
    });
  }
}
