import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class HolidaysService {

  formData ={id:0 , date:'', name:''}
  constructor(private http : HttpClient) { }

getHolidays(){
  return this.http.get(`${environment.baseUrl}/OfficialHolidays`)
}
getById(id: number) {
  return this.http.get(`${environment.baseUrl}/OfficialHolidays/${id}`);
}

postHolidays() {
  return this.http.post(`${environment.baseUrl}/OfficialHolidays`, this.formData);
}

putHoliday() {
  return this.http.put(
    `${environment.baseUrl}/OfficialHolidays/${this.formData.id}`,
    this.formData
  );
}

deleteHoliday(id: number) {
  return this.http.delete(`${environment.baseUrl}/OfficialHolidays/${id}`);
}
}
