import { Component, HostListener } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './core/components/header/header.component';
import { LoginComponent } from './features/Account/login/login.component';
import { RegisterComponent } from './features/Account/register/register.component';
import {
  SocialLoginModule,
  SocialAuthServiceConfig,
} from '@abacritt/angularx-social-login';
import {
  GoogleLoginProvider,
  GoogleSigninButtonDirective,
  GoogleSigninButtonModule,
} from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AuthService } from './features/Account/services/auth.service';
import { SidebarComponent } from './core/components/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    SocialLoginModule,
    FormsModule,
    GoogleSigninButtonModule,
    GoogleSigninButtonModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'WebUI';
  private startTime: number = 0;
  private totalTimeSpent: number = 0;

  constructor(private authService: AuthService) {}
  ngOnInit() {
    this.startTime = Date.now(); // Bắt đầu tính thời gian
    console.log(
      'User entered the app at:',
      new Date(this.startTime).toLocaleString()
    );
  }

  @HostListener('window:beforeunload', ['$event'])
  handleBeforeUnload(event: Event) {
    const endTime = Date.now();
    const totalTimeSpent = (endTime - this.startTime) / 1000; // Thời gian tính bằng giây

    const today = new Date();
    const day = today.getDate();
    const month = today.getMonth() + 1;
    const year = today.getFullYear();
    const dateString = `${month}/${day}/${year}`;
    const timeline = `${dateString}:${totalTimeSpent}`;
    this.authService.editTimeline(timeline).subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
    console.log(`User spent ${this.totalTimeSpent} seconds on the web.`);
  }
}
