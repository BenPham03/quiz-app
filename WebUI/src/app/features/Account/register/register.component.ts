import { Component, NgZone } from '@angular/core';
import { Register } from '../model/register';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { FormBuilder, FormsModule } from '@angular/forms';
import {
  GoogleSigninButtonModule,
  SocialLoginModule,
} from '@abacritt/angularx-social-login';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';

@Component({
  selector: 'app-register',
  imports: [FormsModule, SocialLoginModule, GoogleSigninButtonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  model: Register;

  constructor(
    private authService: AuthService,
    private router: Router,
    private cookieService: CookieService,
    private _ngZone: NgZone,
    private fb: FormBuilder
  ) {
    this.model = {
      Email: '',
      Password: '',
      UserName: '',
    };
  }

  validatePassword(): boolean {
    var passwordCheck = this.model.Password;
    if (passwordCheck.length < 8) {
      alert('Password must be has 8 characters at least!');
      return false;
    }
    if (!/[A-Z]/.test(passwordCheck)) {
      alert('Password must be has 1 uppercase character at least!');
      return false;
    }
    if (!/[a-z]/.test(passwordCheck)) {
      alert('Password must be has 1 lowercase character at least!');
      return false;
    }
    if (!/\d/.test(passwordCheck)) {
      alert('Password must be has 1 digit at least!');
      return false;
    }
    if (!/[!@#$%^&*_?><:";']/.test(passwordCheck)) {
      alert('Password must be has 1 special character at least!');
      return false;
    }
    return true;
  }

  onFormSubmit() {
    if (!this.model.Email || !this.model.Password || !this.model.UserName) {
      alert('Infomation can not null!');
    } else {
      if (this.validatePassword()) {
        this.authService.register(this.model).subscribe({
          next: (res) => {
            alert('Đăng ký thành công vui lòng đăng nhập!');
            this._ngZone.run(() => {
              this.router.navigate(['/login']);
            });
          },
          error: (err) => {
            console.error('Error:', err);
            alert(err.error);
          },
        });
      }
    }
  }

  ngOnInit(): void {
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      google.accounts.id.initialize({
        client_id:
          '317279261159-8p7197blc6u7djrstkcq84repkvg5b2l.apps.googleusercontent.com',
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,
      });

      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById('buttonDiv'),
        { theme: 'outline', size: 'large', width: '800px' }
      );
      google.accounts.id.prompt((notification: PromptMomentNotification) => {});
    };
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.authService.loginWithGoogle(response.credential).subscribe(
      (x) => {
        this.cookieService.set('token', x.token);
        this._ngZone.run(() => {
          this.router.navigate(['/example']);
        });
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
