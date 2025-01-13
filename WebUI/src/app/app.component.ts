import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./core/components/header/header.component";
import { LoginComponent } from "./features/Account/login/login.component";
import { RegisterComponent } from "./features/Account/register/register.component";
import { SocialLoginModule, SocialAuthServiceConfig} from '@abacritt/angularx-social-login';
import { GoogleLoginProvider, GoogleSigninButtonDirective, GoogleSigninButtonModule  } from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, SocialLoginModule, FormsModule,
  GoogleSigninButtonModule,GoogleSigninButtonModule],

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'WebUI';
}
