import { Component } from '@angular/core';
import { LoginService, TokenData } from '../../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  email: string = 'testuser@gmail.com';
  password: string = 'password';

  tokenData: TokenData;

  constructor(readonly service: LoginService) { }

  login() {
    const loginData = { email: this.email, password: this.password };

      this.service.login(loginData)
        .subscribe(tokenData => this.tokenData = tokenData);
  }
}
