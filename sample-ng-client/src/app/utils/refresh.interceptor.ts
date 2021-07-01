import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service';
import { mergeMap } from 'rxjs/operators';

@Injectable()
export class RefreshInterceptor implements HttpInterceptor {

  constructor(readonly authService: LoginService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(request.url.includes(this.authService.authApiLink))
    {
      return next.handle(request);
    }
    
    const isTokenExpired = this.authService.token$.value.expires <= new Date();

    if(isTokenExpired) {
      return this.authService.refresh()
      .pipe(
        mergeMap(tokenModel => {
          const cloned = request.clone({
            headers: new HttpHeaders({
              'Authorization': `Bearer ${tokenModel.jwtToken}`
            })
          });
      
          return next.handle(cloned);
        })
      );
    }

    return next.handle(request);
  }
}
