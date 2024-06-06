import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class BaseService {
    protected readonly http = inject(HttpClient);
    protected apiBaseUrl: string;
    constructor() {
        this.apiBaseUrl = 'https://localhost:44330/api/';
    }
}
