import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

export interface LoginRequest {
    email: string;
    password: string;
}

export interface AuthResponse {
    userId: number;
    userName: string;
    email: string;
    role: string;
    token: string;
}

export interface ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private readonly apiUrl = 'https://localhost:7047/api/Auth';

    constructor(
        private http: HttpClient,
        @Inject(PLATFORM_ID) private platformId: object
    ) { }

    login(request: LoginRequest): Observable<ApiResponse<AuthResponse>> {
        return this.http.post<ApiResponse<AuthResponse>>(
            `${this.apiUrl}/login`,
            request
        );
    }

    saveAuthData(response: AuthResponse): void {
        localStorage.setItem('token', response.token);
        localStorage.setItem('userId', response.userId.toString());
        localStorage.setItem('userName', response.userName);
        localStorage.setItem('email', response.email);
        localStorage.setItem('role', response.role);
    }

    getToken(): string | null {
        if (!isPlatformBrowser(this.platformId)) {
            return null;
        }

        return localStorage.getItem('token');
    }

    getRole(): string | null {
        if (!isPlatformBrowser(this.platformId)) {
            return null;
        }

        return localStorage.getItem('role');
    }

    isLoggedIn(): boolean {
        return !!this.getToken();
    }

    logout(): void {
        if (isPlatformBrowser(this.platformId)) {
            localStorage.clear();
        }
    }
}