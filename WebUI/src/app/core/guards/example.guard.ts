import { CanActivateFn } from '@angular/router';

export const exampleGuard: CanActivateFn = (route, state) => {
  return true;
};
