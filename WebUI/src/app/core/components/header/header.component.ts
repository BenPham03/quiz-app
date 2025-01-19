import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../features/Account/services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  isLogin: boolean = false;
  quizId: string = '';
  constructor(
    private authService: AuthService,
    private router: Router,
    private cookie: CookieService
  ) {}
  logOut() {
    this.authService.logout();
    window.location.reload();
  }
  signIn() {
    this.router.navigate(['/login']);
  }
  ngOnInit() {
    const token = this.cookie.get('token');
    if (!token) {
      this.isLogin = false;
    } else {
      this.isLogin = true;
    }
  }
  navigateTo(page: string) {
    this.router.navigate([`/${page}`]);
  }
  onSubmit() {
    console.log(this.quizId);
    if (this.quizId.trim()) {
      this.router.navigate(['/search', this.quizId]);
    }
  }
}
