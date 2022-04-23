import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root',
})
export class HasRoleGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private messageService: MessageService
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const isAuthenrized = route.data['role'].includes(
      this.authService.currentUser.role
    );
    if (!isAuthenrized) {
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Access Denied',
        detail: 'You do not have permission to access this page',
      });
    }
    return isAuthenrized;
  }
}
