import { Component } from '@angular/core';
import { AuthService } from '../../../features/Account/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header-admin',
  imports: [],
  templateUrl: './header-admin.component.html',
  styleUrl: './header-admin.component.css',
})
export class HeaderAdminComponent {
  constructor(private authService: AuthService, private router: Router) {}
  logOut() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
