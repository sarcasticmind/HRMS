import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class SalarySettingsService {

  formData = { penalityhour: '', extrahour: '' };

  constructor(private http: HttpClient) {}

  getSalSettings() {
    return this.http.get(`${environment.baseUrl}/PenalityExtras`);
  }
  postSalSettings() {
    return this.http.post(`${environment.baseUrl}/PenalityExtras`, this.formData);
  }
}
