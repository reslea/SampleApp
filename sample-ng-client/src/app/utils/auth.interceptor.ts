import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable, empty } from 'rxjs';
import { LoginService } from '../services/login.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: LoginService,
              private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.includes(this.authService.authApiLink))
    {
      return next.handle(request);
    }

    if (!this.authService.token$.value) {
      this.router.navigate(['/login']);
      return empty();
    }

    const cloned = request.clone({
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.authService.token$.value.rawToken}`
      })
    });

    return next.handle(cloned);
  }
}
