import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanActivate {
  constructor(readonly authService: LoginService,
              private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    : Observable<boolean> | boolean {

    const requiredPermissions = next.data.permissions;
    for (const permission of requiredPermissions) {
      const hasPermission = this.authService.token$.value.permissions.includes(permission);

      if (!hasPermission) {
        console.log(`you dont have required permission ${permission}`);
        this.router.navigate(['/login']);
        return false;
      }
    }
    return true;
  }
}
