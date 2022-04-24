import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, throwError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './auth.service';
import { catchError, filter, switchMap, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class TokenInterceptorService implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor(private cookieService: CookieService, private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.cookieService.get('accessToken');
    if (token) {
      req = this.addToken(req, token);
    }
    return next.handle(req).pipe(catchError(err =>{
      if (err.status === 401) {
        return this.handle401Error(req, next);
      }
      else {
        return throwError(err);
      } 
    }));
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        'Authorization': `Bearer ${token}`
      }
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler){
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);
      return this.authService.refreshToken().pipe(
        switchMap((data: any) => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(data.data);
          return next.handle(this.addToken(request, data.data));
        }), catchError(err => {
          return EMPTY;
        }));
    }
    else {
      return this.refreshTokenSubject.pipe(
        filter(res => res != null),
        take(1),
        switchMap(data => {
          return next.handle(this.addToken(request, data));
        }));
    }
  }
  
}
