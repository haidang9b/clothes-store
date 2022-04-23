import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { OrderManagementComponent } from './components/order-management/order-management.component';
import { CategoryManagementComponent } from './components/category-management/category-management.component';
import { ProductManagementComponent } from './components/product-management/product-management.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CustomToastComponent } from './components/custom-toast/custom-toast.component';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RippleModule } from 'primeng/ripple';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DialogCategoryComponent } from './components/dialog-category/dialog-category.component';
import { DialogProductComponent } from './components/dialog-product/dialog-product.component';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ErrorComponent } from './components/error/error.component';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './shared/auth.service';
import { IsAuthenticatedGuard } from './shared/is-authenticated.guard';
import { TokenInterceptorService } from './shared/token-interceptor.service';
import { HeaderClientComponent } from './components/header-client/header-client.component';
import { HomeComponent } from './components/home/home.component';
import { FooterComponent } from './components/footer/footer.component';
import { ProductsComponent } from './components/products/products.component';
import { RegisterComponent } from './components/register/register.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { TrackingComponent } from './components/tracking/tracking.component';
import { PolicyComponent } from './components/policy/policy.component';
import { StoreComponent } from './components/store/store.component';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import {MatSelectModule} from '@angular/material/select';
import { DialogExportOrderComponent } from './components/dialog-export-order/dialog-export-order.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    OrderManagementComponent,
    CategoryManagementComponent,
    ProductManagementComponent,
    CustomToastComponent,
    ConfirmDialogComponent,
    DashboardComponent,
    DialogCategoryComponent,
    DialogProductComponent,
    ProductDetailComponent,
    ErrorComponent,
    LoginComponent,
    HeaderClientComponent,
    HomeComponent,
    FooterComponent,
    ProductsComponent,
    RegisterComponent,
    CheckoutComponent,
    TrackingComponent,
    PolicyComponent,
    StoreComponent,
    ChangePasswordComponent,
    UserManagementComponent,
    DialogExportOrderComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MessagesModule,
    MessageModule,
    ToastModule,
    RippleModule,
    MatDialogModule,
    MatIconModule,
    MatButtonModule,
    MatToolbarModule,
    MatInputModule,
    MatCardModule,
    MatDividerModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    
  ],
  providers: [
    MessageService,
    AuthService,
    IsAuthenticatedGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
  entryComponents: [DialogCategoryComponent],
})
export class AppModule {}
