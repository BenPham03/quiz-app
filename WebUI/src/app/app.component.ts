import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./core/components/header/header.component";
<<<<<<< HEAD
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
=======

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
>>>>>>> 8d4ab35077fd29093f53c5fa7eb6bc86003c1e80
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'WebUI';
}
