import { Injectable } from '@angular/core';
import {Observable, catchError, map, tap, of} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ProductManagementService {
  private productUrl = `${environment.urlApi}products`;
  constructor(
    private http: HttpClient,
  ) { }
  
  getProducts(): Observable<any> {
    return this.http.get(this.productUrl);
  }
  getProductsByCategory(id:number): Observable<any> {
    return this.http.get(`${this.productUrl}/category/${id}`);
  }

  addProduct(product: any): Observable<any> {
    const formData = new FormData();
    formData.append('title', product.title);
    formData.append('description', product.description);
    formData.append('price', product.price.toString());
    formData.append('quantity', product.quantity);
    formData.append('image', product.image);
    formData.append('category_id', product.category.toString());
    return this.http.post(this.productUrl, formData);
  }

  removeProduct(id: number): Observable<any> {
    return this.http.delete(`${this.productUrl}/${id}`);
  }

  updateProduct(product: any): Observable<any> {
    const formData = new FormData();
    formData.append('title', product.title);
    formData.append('description', product.description);
    formData.append('price', product.price.toString());
    formData.append('quantity', product.quantity);
    formData.append('image', product.image);
    formData.append('category_id', product.category.toString());
    return this.http.put(`${this.productUrl}/${product.id}`, formData);
  }

  getNewArrivals(page: number): Observable<any>{
    return this.http.get(`${this.productUrl}/new-arrivals/${page}`);
  }

  getProduct(id: number): Observable<any> {
    return this.http.get(`${this.productUrl}/${id}`);
  }
}
