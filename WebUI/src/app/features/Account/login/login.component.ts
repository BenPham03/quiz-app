import { Component, NgZone, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Login } from '../model/login';
import { jwtDecode } from 'jwt-decode';
import { FormsModule, FormBuilder, Validators } from '@angular/forms';
import {  SocialUser,SocialLoginModule ,GoogleSigninButtonModule  } from '@abacritt/angularx-social-login';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { SendEmailRequest } from '../model/send-email';
import { ForgotPasswordModalComponent } from "../forgot-password-modal/forgot-password-modal.component";
import { UpdatePasswordModalComponent } from "../update-password-modal/update-password-modal.component";

@Component({
  selector: 'app-login',
  imports: [FormsModule, SocialLoginModule, GoogleSigninButtonModule, ForgotPasswordModalComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  model : Login

  constructor(private authservice : AuthService,
    private router : Router,
    private cookieService : CookieService,
     private _ngZone: NgZone,
     private fb: FormBuilder
  ) {
    this.model ={
      Email : '',
      Password : ''
    }

  }
  decodeToken(token : string)
  {
    try{
      const decoded : any = jwtDecode(token);
      return decoded
    }
    catch(error)
    {
      console.log("Error to decode token")
      return null
    }
  }
  getRole(token : string)
  {
    const decodedToken = this.decodeToken(token);
  if (decodedToken) {
    return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]; // Extract the role
  }
  return null; // If no role found
  }
  onFormSubmit(){
    this.authservice.login(this.model).subscribe(
      {
        next: response =>{
          this.cookieService.set("token",
            `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');
          if(response.token)
          {
            if(this.getRole(response.token) == "Admin")
            {
              this.router.navigateByUrl('/Admin');
            }
            if(this.getRole(response.token) == "User")
            {
              this.router.navigateByUrl('/');
            }
            else{
              this.router.navigateByUrl('/');
            }
          }
        },
        error: error => {
          alert("Wrong email or password")
        }
      }
    )
  }
  ngOnInit() : void{
  if (typeof google !== 'undefined') {
    // @ts-ignore
      window.onGoogleLibraryLoad = () => {
      this.initializeGoogleSignIn();}
    } else {
      // @ts-ignore
      window.onGoogleLibraryLoad = () => {
        this.initializeGoogleSignIn();
      };
    }
  
}

  ngAfterViewInit(): void {
    this.initializeGoogleSignIn()
    setTimeout(() => {
      google.accounts.id.renderButton(
      // @ts-ignore
      document.getElementById('buttonDiv'),
      { theme: 'outline', size: 'large',width : '800px'}
      
    );
    }, 100);
    
  }
  initializeGoogleSignIn(){
    google.accounts.id.initialize({
      client_id: '317279261159-8p7197blc6u7djrstkcq84repkvg5b2l.apps.googleusercontent.com',
      callback : this.handleCredentialResponse.bind(this),
      auto_select: false,
      cancel_on_tap_outside: true
    });

    this.renderGoogleButton()
    google.accounts.id.prompt((notification: PromptMomentNotification) =>{}); 
  }
  renderGoogleButton(){
    google.accounts.id.renderButton(
      // @ts-ignore
      document.getElementById('buttonDiv'),
      { theme: 'outline', size: 'large',width : '800px'}
      
    );
  }
async handleCredentialResponse(response: CredentialResponse){

  await this.authservice.loginWithGoogle(response.credential).subscribe(

    x =>{
    console.log(x.token)
      this.cookieService.set("token", x.token); 
      this._ngZone.run(() =>{
        this.router.navigate(['/home'])
      })
    },
    (error: any) =>{
          console.log(error)
    }
  )
}

}
