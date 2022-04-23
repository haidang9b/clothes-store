import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryManagementComponent } from './components/category-management/category-management.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { OrderManagementComponent } from './components/order-management/order-management.component';
import { PolicyComponent } from './components/policy/policy.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductManagementComponent } from './components/product-management/product-management.component';
import { ProductsComponent } from './components/products/products.component';
import { RegisterComponent } from './components/register/register.component';
import { StoreComponent } from './components/store/store.component';
import { TrackingComponent } from './components/tracking/tracking.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { HasRoleGuard } from './shared/has-role.guard';
import { IsAuthenticatedGuard } from './shared/is-authenticated.guard';
import { IsNotAuthenticatedGuard } from './shared/is-not-authenticated.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'order-management',
    component: OrderManagementComponent,
    canActivate: [IsAuthenticatedGuard, HasRoleGuard],
    data: {
      role: ['Admin', 'Seller'],
    },
  },
  {
    path: 'category-management',
    component: CategoryManagementComponent,
    canActivate: [IsAuthenticatedGuard, HasRoleGuard],
    data: {
      role: ['Admin', 'Seller'],
    },
  },
  {
    path: 'product-management',
    component: ProductManagementComponent,
    canActivate: [IsAuthenticatedGuard, HasRoleGuard],
    data: {
      role: ['Admin', 'Seller'],
    },
  },
  {
    path: 'user-management',
    component: UserManagementComponent,
    canActivate: [IsAuthenticatedGuard, HasRoleGuard],
    data: {
      role: ['Admin'],
    },
  },
  {
    path: 'products',
    component: ProductsComponent,
    pathMatch: 'full',
  },
  {
    path: 'tracking-order/:id',
    component: TrackingComponent,
  },
  {
    path: 'product-detail/:id',
    component: ProductDetailComponent,
  },
  {
    path: 'change-password',
    component: ChangePasswordComponent,
    canActivate: [IsAuthenticatedGuard],
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'policy',
    component: PolicyComponent,
  },
  {
    path: 'store',
    component: StoreComponent,
  },
  {
    path: 'checkout',
    component: CheckoutComponent,
  },
  {
    path: 'error',
    component: ErrorComponent,
  },
  {
    path: '**',
    component: ErrorComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
