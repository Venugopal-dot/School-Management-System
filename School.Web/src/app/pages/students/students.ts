import {
  Component,
  Inject,
  PLATFORM_ID,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import {
  CommonModule,
  isPlatformBrowser
} from '@angular/common';

import { FormsModule } from '@angular/forms';

import { Navbar } from '../../shared/navbar/navbar';

import {
  StudentService,
  Student
} from '../../services/student';

@Component({
  selector: 'app-students',
  imports: [
    CommonModule,
    FormsModule,
    Navbar
  ],
  templateUrl: './students.html',
  styleUrl: './students.css'
})
export class Students implements OnInit {

  students: Student[] = [];

  isLoading = false;

  errorMessage = '';

  successMessage = '';

  showAddForm = false;

  isSaving = false;

  newStudent = {
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    gender: 1,
    email: '',
    phoneNumber: '',
    address: '',
    classRoomId: 0
  };

  isEditMode = false;
  editingStudentId: number | null = null;



  constructor(
    private studentService: StudentService,
    private cdr: ChangeDetectorRef,
    @Inject(PLATFORM_ID) private platformId: object
  ) { }


  ngOnInit(): void {

    if (isPlatformBrowser(this.platformId)) {
      this.loadStudents();
    }

  }


  loadStudents(): void {

    this.isLoading = true;

    this.errorMessage = '';

    this.studentService.getStudents().subscribe({

      next: (response) => {

        console.log(
          'Students API response:',
          response
        );

        this.students = response.data;

        this.isLoading = false;

        console.log(
          'Students assigned to UI:',
          this.students
        );

        this.cdr.detectChanges();

      },

      error: (error: any) => {

        console.error(
          'Student API error:',
          error
        );

        this.errorMessage =
          error?.error?.message ||
          'Unable to load students.';

        this.isLoading = false;

        this.cdr.detectChanges();

      }

    });

  }


  openAddForm(): void {
    this.showAddForm = true;
    this.isEditMode = false;
    this.editingStudentId = null;

    this.successMessage = '';
    this.errorMessage = '';

    this.newStudent = {
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      gender: 1,
      email: '',
      phoneNumber: '',
      address: '',
      classRoomId: 0
    };
  }

  closeAddForm(): void {

    this.showAddForm = false;

    this.isEditMode = false;

    this.editingStudentId = null;

    this.errorMessage = '';
  }




  saveStudent(): void {

    this.errorMessage = '';
    this.successMessage = '';

    // Required field validation
    if (
      !this.newStudent.firstName.trim() ||
      !this.newStudent.lastName.trim() ||
      !this.newStudent.dateOfBirth ||
      !this.newStudent.email.trim() ||
      !this.newStudent.phoneNumber.trim() ||
      !this.newStudent.address.trim() ||
      this.newStudent.classRoomId <= 0
    ) {
      this.errorMessage =
        'Please fill in all required fields.';
      return;
    }

    // Phone validation
    if (!/^[0-9]{10}$/.test(this.newStudent.phoneNumber)) {
      this.errorMessage =
        'Phone number must contain exactly 10 digits.';
      return;
    }

    this.isSaving = true;

    // EDIT MODE
    if (
      this.isEditMode &&
      this.editingStudentId !== null
    ) {

      this.studentService
        .updateStudent(
          this.editingStudentId,
          this.newStudent
        )
        .subscribe({

          next: (response) => {

            console.log(
              'Student updated:',
              response
            );

            this.successMessage =
              'Student updated successfully.';

            this.isSaving = false;

            this.showAddForm = false;

            this.isEditMode = false;

            this.editingStudentId = null;

            this.resetStudentForm();

            this.loadStudents();

            this.cdr.detectChanges();
          },

          error: (error: any) => {

            console.error(
              'Update student error:',
              error
            );

            this.isSaving = false;

            this.errorMessage =
              error?.error?.message ||
              'Unable to update student.';

            this.cdr.detectChanges();
          }

        });

      return;
    }


    // ADD MODE
    this.studentService
      .createStudent(this.newStudent)
      .subscribe({

        next: (response) => {

          console.log(
            'Student created:',
            response
          );

          this.successMessage =
            'Student created successfully.';

          this.isSaving = false;

          this.showAddForm = false;

          this.resetStudentForm();

          this.loadStudents();

          this.cdr.detectChanges();
        },

        error: (error: any) => {

          console.error(
            'Create student error:',
            error
          );

          this.isSaving = false;

          this.errorMessage =
            error?.error?.message ||
            'Unable to create student.';

          this.cdr.detectChanges();
        }

      });
  }

  resetStudentForm(): void {

    this.newStudent = {
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      gender: 1,
      email: '',
      phoneNumber: '',
      address: '',
      classRoomId: 0
    };

  }

  deleteStudent(student: Student): void {

    const confirmed = window.confirm(
      `Are you sure you want to delete ${student.firstName} ${student.lastName}?`
    );

    if (!confirmed) {
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';

    this.studentService.deleteStudent(student.id).subscribe({
      next: () => {

        console.log('Delete API success. Deleted ID:', student.id);

        // Remove student from UI list
        this.students = this.students.filter(
          s => s.id !== student.id
        );

        this.successMessage = 'Student deleted successfully.';

        this.cdr.detectChanges();

        console.log('Students after delete:', this.students);

        this.successMessage =
          'Student deleted successfully.';

        // Force Angular to update the view
        this.cdr.markForCheck();

        setTimeout(() => {
          this.cdr.detectChanges();
        }, 0);
      },

      error: (error: any) => {

        console.error('Delete student error:', error);

        this.errorMessage =
          error?.error?.message ||
          'Unable to delete student.';

        this.cdr.markForCheck();

        setTimeout(() => {
          this.cdr.detectChanges();
        }, 0);
      }
    });
  }

  editStudent(student: Student): void {

    this.errorMessage = '';
    this.successMessage = '';

    this.isEditMode = true;
    this.editingStudentId = student.id;
    this.showAddForm = true;

    this.studentService
      .getStudentById(student.id)
      .subscribe({

        next: (response) => {

          const data = response.data;

          this.newStudent = {
            firstName: data.firstName,
            lastName: data.lastName,
            dateOfBirth: data.dateOfBirth
              ? data.dateOfBirth.substring(0, 10)
              : '',
            gender: data.gender,
            email: data.email,
            phoneNumber: data.phoneNumber,
            address: data.address,
            classRoomId: data.classRoomId
          };

          this.cdr.detectChanges();
        },

        error: (error: any) => {

          console.error(
            'Get student details error:',
            error
          );

          this.errorMessage =
            error?.error?.message ||
            'Unable to load student details.';

          this.showAddForm = false;

          this.cdr.detectChanges();
        }
      });
  }

}