import { Injectable } from '@angular/core';
import {Observable, catchError, map, tap, of} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class UploadService {
  private uploadUrl = `${environment.urlApi}uploads`;
  constructor(
    private http: HttpClient,
  ) { }
  uploadFile(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(`${this.uploadUrl}/upload`, formData);
  }
}
