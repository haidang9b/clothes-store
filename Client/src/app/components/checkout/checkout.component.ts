import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CartService } from '../../shared/cart.service';
import { AuthService } from '../../shared/auth.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  totalPrice = 0;
  products: any[] = [];
  checkoutFormGroup: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    numberPhone: new FormControl({value: '', disabled: false}, [Validators.required, Validators.minLength(9), Validators.maxLength(11)]),
    province: new FormControl({value: '', disabled: false}, [Validators.required]),
    district: new FormControl({value: '', disabled: false}, [Validators.required]),
    ward: new FormControl({value: '', disabled: false}, [Validators.required]),
    addressDetails: new FormControl({value: '', disabled: false}, [Validators.required])
  })
  constructor(
    private cartService: CartService,
    private authService: AuthService, 
    private messageService: MessageService,
    private router: Router
  ) { 
    this.products = this.cartService.getCartItems();
    console.log(this.products);
  }
  onSubmit() {
    if(this.checkoutFormGroup.valid == false){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Failed',
        detail: 'Please fill all required fields',
        life:3000
      })
      return;
    }
    
    var address = `${this.checkoutFormGroup.controls['province'].value} - ${this.checkoutFormGroup.controls['district'].value} - ${this.checkoutFormGroup.controls['ward'].value} - ${this.checkoutFormGroup.controls['addressDetails'].value}`;
    var obj = {
      products: this.products,
      nameReceiver: this.checkoutFormGroup.controls['name'].value,
      numberPhone: this.checkoutFormGroup.controls['numberPhone'].value,
      address: address,
    };
    this.cartService.addBill(obj).subscribe(res => {
      if(res.isSuccess){
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: 'Your order has been sent, your id order is ' + res.data + ". Please wait for our call",
          life:10000
        })
        this.cartService.resetCart();
        this.router.navigate([`/tracking-order/${res.data}`]);
      }
      else{
        this.messageService.add({
          key: 'tr',
          severity: 'error',
          summary: 'Failed',
          detail: 'Please try again',
          life:3000
        })
      }
    })
  }
  removeItemInCart(index: any): void{
    this.cartService.removeItem(index);
    this.products = this.cartService.getCartItems();
    this.getTotalPrice();
    if(this.products.length === 0){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Please add product to cart',
        life:3000
      })
      this.router.navigate(['/']);
      return;
    }
  }
  ngOnInit(): void {
    if(this.products.length === 0){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Please add product to cart',
        life:3000
      })
      this.router.navigate(['/']);
      return;
    }
    this.getTotalPrice();
  }
  

  private getTotalPrice(){
    this.totalPrice = 0;
    this.products.forEach(element => {
      this.totalPrice += element.price;
    });
  }

}
