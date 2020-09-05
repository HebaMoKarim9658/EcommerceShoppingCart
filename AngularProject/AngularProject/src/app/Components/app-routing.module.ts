import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { ProductsComponent } from './products/products.component';
import { LoginComponent } from './login/login.component';
import { ErrorComponent } from './error/error.component';
import { CategoriesService } from '../Services/categories.service';
import { ShoppingCartComponent } from './ShoppingCart/shopping-cart/shopping-cart.component';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './profile/edit-profile/edit-profile.component';
import { RegistrationComponent } from './registration/registration.component'
import { CreateProductComponent } from './create-product/create-product.component'
import { AdminProductComponent } from './Admin/admin-product/admin-product.component';
import { UpdateProductComponent } from './Admin/update-product/update-product.component';
import { AdminordersComponent } from './Admin/admin-orders/adminorders/adminorders.component';
import { AuthGuardAdminService } from '../Services/auth-guard-admin.service';
import { AuthGuardUserService } from '../Services/auth-guard-user.service';

const routes: Routes = [
  { path: '', redirectTo: 'product', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'product', component: ProductsComponent, canActivate: [AuthGuardUserService] },
  { path: 'product/:id', component: ProductsComponent, canActivate: [AuthGuardUserService] },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuardUserService] },
  { path: 'editProfile', component: EditProfileComponent, canActivate: [AuthGuardUserService] },
  { path: 'Registration', component: RegistrationComponent },
  { path: 'CreateProduct', component: CreateProductComponent, canActivate: [AuthGuardAdminService] },
  { path: 'admin', component: AdminProductComponent, canActivate: [AuthGuardAdminService] },
  { path: 'updateProduct/:id', component: UpdateProductComponent, canActivate: [AuthGuardAdminService] },
  { path: 'shoppingCart', component: ShoppingCartComponent, canActivate: [AuthGuardUserService] },
  { path: 'adminorders', component: AdminordersComponent, canActivate: [AuthGuardAdminService] },
  { path: '**', component: ErrorComponent },
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
