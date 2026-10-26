import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Student {
    id: number;
    admissionNumber: string;
    firstName: string;
    lastName: string;
    email: string;
}

export interface Student {
    id: number;
    admissionNumber: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    gender: number;
    email: string;
    phoneNumber: string;
    address: string;
    classRoomId: number;
}

export interface CreateStudentRequest {
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    gender: number;
    email: string;
    phoneNumber: string;
    address: string;
    classRoomId: number;
}

export interface ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;
}

export interface UpdateStudentRequest {
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    gender: number;
    email: string;
    phoneNumber: string;
    address: string;
    classRoomId: number;
}

@Injectable({
    providedIn: 'root'
})
export class StudentService {

    private readonly apiUrl =
        'https://localhost:7047/api/Student';

    constructor(
        private http: HttpClient
    ) { }

    getStudents(): Observable<ApiResponse<Student[]>> {

        return this.http.get<ApiResponse<Student[]>>(
            this.apiUrl
        );

    }

    createStudent(
        request: CreateStudentRequest
    ): Observable<ApiResponse<Student>> {

        return this.http.post<ApiResponse<Student>>(
            this.apiUrl,
            request
        );

    }

    updateStudent(
        id: number,
        request: UpdateStudentRequest
    ): Observable<ApiResponse<Student>> {
        return this.http.put<ApiResponse<Student>>(
            `${this.apiUrl}/${id}`,
            request
        );
    }
    getStudentById(
        id: number
    ): Observable<ApiResponse<Student>> {
        return this.http.get<ApiResponse<Student>>(
            `${this.apiUrl}/${id}`
        );
    }

    deleteStudent(id: number): Observable<void> {
        return this.http.delete<void>(
            `${this.apiUrl}/${id}`
        );
    }

}