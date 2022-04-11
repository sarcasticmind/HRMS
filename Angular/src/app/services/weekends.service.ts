import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class WeekendsService {
  formData = { weekend1: '', weekend2: '' };
  constructor(private http: HttpClient) {}

  getWeekend() {
    return this.http.get(`${environment.baseUrl}/WeekEnds`);
  }
  postWeekend() {
    return this.http.post(`${environment.baseUrl}/WeekEnds`, this.formData);
  }
}
