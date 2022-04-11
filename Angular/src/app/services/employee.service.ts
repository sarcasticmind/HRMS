import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { Employee } from '../Models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  formData : Employee = new Employee()
  constructor(private http : HttpClient) { }

getAllEmployees(){
  return this.http.get(`${environment.baseUrl}/Employees`)
}
getById(id: number) {
  return this.http.get(`${environment.baseUrl}/Employees/${id}`);
}

addEmployee() {
  return this.http.post(`${environment.baseUrl}/Employees`, this.formData);
}

updateEmployee() {
  return this.http.put(
    `${environment.baseUrl}/Employees/${this.formData.id}`,
    this.formData
  );
}

deleteEmployee(id: number) {
  return this.http.delete(`${environment.baseUrl}/Employees/${id}`);
}
}
