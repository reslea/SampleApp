import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';


export interface TokenModel {
  jwtToken: string,
  refreshToken: string
}

export interface TokenData {
  email: string,
  permissions: string[],
  expires: Date,
  rawToken: string
}

export const permissions = { 
  readBooks: 'ReadBooks',
  editBooks: 'EditBooks'
}

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  authApiLink = "https://localhost:6001/api/auth";
  tokenKey = 'jwtToken';

  token$ = new BehaviorSubject<TokenData>(null);
  refreshToken$ = new BehaviorSubject<string>(null);

  constructor(private http: HttpClient) {
    // const rawToken = localStorage.getItem(this.tokenKey);
    // if(rawToken) {
    //   const tokenData = this.getTokenData(rawToken);
    //   this.token$.next(tokenData);
    // }
   }

  login(loginData) {
    return this.http.post<TokenModel>(this.authApiLink, loginData)
      .pipe(
        tap(model => {
          this.refreshToken$.next(model.refreshToken);
          this.token$.next(this.getTokenData(model.jwtToken));
          // localStorage.setItem(this.tokenKey, model.jwtToken);
        }),
        map(model => this.getTokenData(model.jwtToken)),
      )
  }

  refresh() {
    return this.http.post<TokenModel>(`${this.authApiLink}/refresh`, 
    { 
      refreshToken: this.refreshToken$.value 
    });
  }

  getTokenData(token: string): TokenData {
    const payloadBase64 = token.split('.')[1];
    const payload = JSON.parse(atob(payloadBase64));
  
    const permissions = Array.isArray(payload.permission) ? payload.permission : [payload.permission];
  
    return {
      email: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] as string,
      permissions: permissions as string[], 
      expires: new Date(payload.exp * 1000),
      rawToken: token
    };
  }
}