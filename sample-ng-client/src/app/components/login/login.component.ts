import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

interface TokenModel {
  jwtToken: string,
  refreshToken: string
}

interface TokenData {
  email: string,
  permissions: string[],
  expires: Date,
  rawToken: string
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  authApiLink = "https://localhost:6001/api/auth";

  email: string = 'testuser@gmail.com';
  password: string = 'password';

  constructor(private http: HttpClient) { }

  login() {
    const loginData = { email: this.email, password: this.password };

    this.http.post<TokenModel>(this.authApiLink, loginData)
      .pipe(
        map(model => this.getTokenData(model.jwtToken))
      )
      .subscribe(tokenData => console.log(tokenData));
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
