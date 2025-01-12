// ng generate interceptor interceptors/auth  ===============  npm install ngx-cookie-service
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token=inject(CookieService).get('token');

  if(token){
        const clonedRequest=req.clone({
          setHeaders:{
            Authorization:'Bearer '+token
          },
        });
        return next(clonedRequest).pipe(
          catchError((error) => {
            if (error.status === 401) {
              // Token hết hạn hoặc không hợp lệ
              console.error('Unauthorized request. Token might be invalid.');
            }
            return throwError(() => error);
          })
        );
      }
  
      return next(req)
};