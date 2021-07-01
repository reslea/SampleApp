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

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authService: LoginService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(request.url.includes(this.authService.authApiLink))
    {
      return next.handle(request);      
    }

    const cloned = request.clone({
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.authService.token$.value.rawToken}`
      })
    });

    return next.handle(cloned);    
  }
}
