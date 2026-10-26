import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = () => {

  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);

  // During SSR/prerendering, allow the route.
  if (!isPlatformBrowser(platformId)) {
    return true;
  }

  const token = window.localStorage.getItem('token');

  console.log('AuthGuard token:', token);

  if (token && token.trim().length > 0) {
    return true;
  }

  console.log('AuthGuard: No token, redirecting to login');

  return router.createUrlTree(['/login']);
};