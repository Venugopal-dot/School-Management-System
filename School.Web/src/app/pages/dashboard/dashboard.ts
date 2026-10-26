import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';
import { StudentService, Student } from '../../services/student';
import { Navbar } from '../../shared/navbar/navbar';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, Navbar],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  userName = '';
  role = '';

  students: Student[] = [];
  isLoadingStudents = false;
  studentError = '';

  constructor(
    private authService: AuthService,
    private studentService: StudentService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      this.userName = localStorage.getItem('userName') ?? '';
      this.role = localStorage.getItem('role') ?? '';

      this.loadStudents();
    }
  }

  loadStudents(): void {
    this.isLoadingStudents = true;
    this.studentError = '';

    this.studentService.getStudents().subscribe({
      next: (response) => {
        this.students = response.data;
        this.isLoadingStudents = false;
      },
      error: (error) => {
        console.error('Student API error:', error);

        this.studentError =
          error?.error?.message ||
          'Unable to load students.';

        this.isLoadingStudents = false;
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}