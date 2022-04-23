import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import * as moment from 'moment';
@Injectable({
  providedIn: 'root',
})
export class CartService {
  apiUrl = `${environment.urlApi}bills`;
  productsInCart: any = [];
  public cartItems: any = [];
  constructor(private http: HttpClient) {}

  addToCart(item: any) {
    console.log(item);
    this.handleData();
    this.cartItems.push(item);
    localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  }

  resetCart() {
    this.cartItems = [];
    localStorage.removeItem('cartItems');
  }

  removeItem(index: any) {
    this.handleData();
    console.log(this.cartItems);
    if (this.cartItems.length < index) {
      return;
    }
    var items = [];
    for (var i = 0; i < this.cartItems.length; i++) {
      if (i != index) {
        items.push(this.cartItems[i]);
      }
    }
    this.cartItems = items;
    localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  }

  getCartItems(): any {
    this.handleData();
    return this.cartItems;
  }

  countProductInCart(): number {
    return this.cartItems.length;
  }

  exportOrder(obj: any) {
    var formData = new FormData();
    formData.append('start', obj.start);
    formData.append('end', obj.end);
    return this.http.post(`${this.apiUrl}/export-order`, formData, {responseType: "arraybuffer"}).toPromise().then((response) =>{
      const blob = new Blob([response as BlobPart], {type:'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'});
      var fileName =  moment().format('YYYY-MM-DD') + "_"+ 'orders.xlsx';

      if(window.navigator &&(window.navigator as any).msSaveOrOpenBlob) {
        (window.navigator as any).msSaveOrOpenBlob(blob, fileName);
      }
      else{
        const downloadUrl = window.URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.setAttribute('style', 'display: none');
        anchor.download = fileName;
        document.body.appendChild(anchor);
        anchor.href = downloadUrl;
        anchor.click();
      }
      
    });
  }

  private handleData() {
    var jsonData = localStorage.getItem('cartItems') || '[]';
    if (this.isJSONType(jsonData)) {
      this.cartItems = JSON.parse(jsonData);
    } else {
      localStorage.removeItem('cartItems');
      this.cartItems = [];
    }
  }

  private isJSONType(str: string): boolean {
    try {
      JSON.parse(str);
    } catch (e) {
      return false;
    }
    return true;
  }

  private handleProductInCart() {
    this.handleData();
    this.productsInCart = [];
    for (var i = 0; i < this.cartItems.length; i++) {
      this.productsInCart.push(this.cartItems[i].id);
    }
  }

  cartIsEmpty(): boolean {
    this.handleData();
    return this.cartItems.length === 0;
  }

  removeFromCart(item: any) {
    this.cartItems = this.cartItems.filter(
      (p: { id: any }) => p.id !== item.id
    );
  }

  addBill(obj: any): Observable<any> {
    this.handleProductInCart();
    return this.http.post(`${this.apiUrl}`, {
      products: this.productsInCart,
      nameReceiver: obj.nameReceiver,
      numberPhone: obj.numberPhone,
      address: obj.address,
    });
  }

  getOrderItems(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  getOrderItemByID(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  changeStatusOrder(obj: any): Observable<any> {
    var formData = new FormData();
    formData.append('status', obj.status);
    return this.http.put(`${this.apiUrl}/change-status/${obj.id}`, formData);
  }
}
