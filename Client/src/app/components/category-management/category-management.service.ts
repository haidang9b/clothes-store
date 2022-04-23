import { Injectable } from '@angular/core';
import {Observable, catchError, map, tap, of} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryManagementService {

  private categoryUrl = `${environment.urlApi}categories`;
  getCategories(): Observable<any> {
    return this.http.get(this.categoryUrl);
  }
  addCategory(obj: any): Observable<any> {
    const formData = new FormData();
    formData.append('name', obj.name);
    return this.http.post(this.categoryUrl, formData);
  }
  getCategoryByID(id: number): Observable<any> {
    return this.http.get(`${this.categoryUrl}/${id}`);
  }
  removeCategory(id: number): Observable<any> {
    return this.http.delete(`${this.categoryUrl}/${id}`);
  }
  updateCategory(obj: any): Observable<any> {
    const formData = new FormData();
    formData.append('name', obj.name);
    return this.http.put(`${this.categoryUrl}/${obj.id}`, formData);
  }
  constructor(
    private http: HttpClient,
  ) { }
}
