import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Login } from '../model/login';
import { Observable, Subject } from 'rxjs';
import { LoginResponse } from '../model/login-response';
import { Base_URL } from '../../../app.config';
import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { Router } from '@angular/router';
import { Register } from '../model/register';
import { SendEmailRequest } from '../model/send-email';
import { updatePasswordRequest } from '../model/uppdate-password';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authChangeSub = new Subject<boolean>();
  private extAuthChangeSub = new Subject<SocialUser>();
  public authChanged = this.authChangeSub.asObservable();
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  user: SocialUser | undefined;
  constructor(private http: HttpClient,
    private cookieService: CookieService,
  private socialAuthService: SocialAuthService,
    private router : Router,
) { 
    this.socialAuthService.authState.subscribe((user) => {
      console.log(user)
      this.extAuthChangeSub.next(user);
    })
  }

    login(loginRequest : Login) : Observable<LoginResponse>{
      return this.http.post<LoginResponse>(`${Base_URL}/Authentication/login`, loginRequest)
    }
    
     loginWithGoogle(credencial: string): Observable<LoginResponse> {
      const header = new HttpHeaders().set('Content-type', 'application/json');
      let a=  this.http.post<LoginResponse>(`${Base_URL}/Authentication/google-login`, JSON.stringify(credencial), {headers: header})
      console.log(a)
      return a
    }
    
    register(registerRequest : Register) : Observable<any>{
      return this.http.post(`${Base_URL}/Authentication/register`, registerRequest)
    }

    sendMail(sendEmailRequest  : SendEmailRequest) : Observable<any>{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      return this.http.post(`${Base_URL}/Mail/send-email`, sendEmailRequest,{headers})
    }

    updatePassword(updatePasswordRequest : updatePasswordRequest) : Observable<any>{
      console.log(updatePasswordRequest)
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      return this.http.post(`${Base_URL}/Authentication/update-password`, updatePasswordRequest,{headers})
    }
    logout() {
    this.cookieService.delete('token')
    this.socialAuthService.signOut();
  }
}
