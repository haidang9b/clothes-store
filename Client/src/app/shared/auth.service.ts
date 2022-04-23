import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { User } from 'src/models/user';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();
  currentUser!: any;
  urlApi = `${environment.urlApi}user`;

  get token(): any {
    return this.cookieService.get('crsfToken');
  }

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private cookieService: CookieService
  ) {
    this._isLoggedIn$.next(!!this.token);
    this.currentUser = this.getUser(this.token);
  }

  login(obj: any): Observable<any> {
    return this.http.post(`${this.urlApi}/login`, obj).pipe(
      tap(async (res: any) => {
        if (res.isSuccess) {
          // console.log(res.data)
          this.currentUser = await this.getUser(res.data.token);
          this.cookieService.set('crsfToken', res.data.token);
          this._isLoggedIn$.next(true);
        }
      })
    );
  }

  logout() {
    this._isLoggedIn$.next(false);
    this.isLoggedIn$ = this._isLoggedIn$.asObservable();
    this.currentUser = null;
    this.cookieService.delete('crsfToken');
    this.router.navigate(['/login']);
  }

  register(obj: any): Observable<any> {
    var formData = new FormData();
    formData.append('username', obj.username);
    formData.append('password', obj.password);
    formData.append('fullName', obj.fullName);
    return this.http.post(`${this.urlApi}/register`, formData);
  }

  getUsers(): Observable<any> {
    return this.http.get(`${this.urlApi}/get-users`);
  }

  getRoles(): Observable<any> {
    return this.http.get(`${this.urlApi}/roles`);
  }

  changePermission(obj: any): Observable<any> {
    var formData = new FormData();
    formData.append('user_id', obj.user_id);
    formData.append('role_id', obj.role_id);
    return this.http.post(`${this.urlApi}/change-roles`, formData);
  }

  changePassword(obj: any): Observable<any> {
    var formData = new FormData();
    formData.append('username', this.currentUser.username);
    formData.append('passwordOld', obj.passwordOld);
    formData.append('passwordNew', obj.passwordNew);
    return this.http.post(`${this.urlApi}/change-password`, formData);
  }

  setIslogged(isLoggedIn: boolean) {
    this._isLoggedIn$.next(isLoggedIn);
  }

  loggedIn() {
    return !!this.token;
  }

  private getUser(token: any) {
    try {
      var base64Url = token.split('.')[1];
      var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      var jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join('')
      );

      let obj = JSON.parse(jsonPayload);
      var user = new User();
      user.fullname = obj['given_name'];
      user.role = obj['role'];
      user.username = obj['unique_name'];
      return user;
    } catch (e) {
      console.log(e);
      return new User();
    }
  }
}
