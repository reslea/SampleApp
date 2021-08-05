import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable, empty } from 'rxjs';
import { LoginService } from '../services/login.service';
import { mergeMap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class RefreshInterceptor implements HttpInterceptor {

  constructor(readonly authService: LoginService,
              private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.includes(this.authService.authApiLink))
    {
      return next.handle(request);
    }

    if (!this.authService.token$.value || !this.authService.refreshToken$.value) {
      this.router.navigate(['/login']);
      return empty();
    }

    const isTokenExpired = this.authService.token$.value.expires <= new Date();

    if (isTokenExpired) {
      return this.authService.refresh()
      .pipe(
        mergeMap(tokenData => {
          const cloned = request.clone({
            headers: new HttpHeaders({
              Authorization: `Bearer ${tokenData.rawToken}`
            })
          });

          return next.handle(cloned);
        })
      );
    }

    return next.handle(request);
  }
}
