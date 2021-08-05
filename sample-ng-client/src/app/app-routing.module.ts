import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { PermissionGuard } from './utils/permission.guard';
import { Permission } from './services/login.service';
import { BooksPageComponent } from './pages/books-page/books-page.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', pathMatch: 'full',
    component: BooksPageComponent,
    canActivate: [PermissionGuard],
      data: {
      permissions: [Permission.readBooks]
    },
 },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [PermissionGuard]
})
export class AppRoutingModule { }
