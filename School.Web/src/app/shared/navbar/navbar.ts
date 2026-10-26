import {
  Component,
  Inject,
  PLATFORM_ID
} from '@angular/core';

import {
  isPlatformBrowser,
  CommonModule
} from '@angular/common';

import {
  Router,
  RouterLink
} from '@angular/router';

import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {

  userName = '';
  role = '';

  constructor(
    private authService: AuthService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      this.userName = localStorage.getItem('userName') ?? '';
      this.role = localStorage.getItem('role') ?? '';
    }
  }

  isAdmin(): boolean {
    return this.role === 'Admin';
  }

  isTeacher(): boolean {
    return this.role === 'Teacher';
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}