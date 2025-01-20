import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);

  let token = cookieService.get('token');
  if (token) {
    token = token.replace('Bearer ', '');

    const decodeToken: any = jwtDecode(token);

    const expirationDate = decodeToken.exp * 1000;
    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      authService.logout();

      return router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url },
      });
    }
    return true;
  }
  authService.logout();
  return router.createUrlTree(['/login'], {
    queryParams: { returnUrl: state.url },
  });
};
