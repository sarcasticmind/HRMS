import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { Attend } from '../Models/attend';

@Injectable({
  providedIn: 'root'
})
export class AttendenceService {
formData : Attend = new Attend()
  constructor(private http:HttpClient) { }

getAll(){
  return this.http.get(`${environment.baseUrl}/Attendances`)
}
getById(id: number) {
  return this.http.get(`${environment.baseUrl}/Attendances/${id}`);
}

add() {
  return this.http.post(`http://localhost:34733/api/Attendances`, this.formData);
}

put(){
  return this.http.put(
    `${environment.baseUrl}/Attendances/${this.formData.id}`,
    this.formData
  );
}

getFiltered(start:string , end:string , name:string){
return this.http
.get(`${environment.baseUrl}/Attendances/Filter/${start}?EndDay=${end}&name=${name}`)
}
delete(id: number) {
  return this.http.delete(`${environment.baseUrl}/Attendances/${id}`);
}
getHolidays(){
  return this.http.get(`${environment.baseUrl}/OfficialHolidays`)
}

}

