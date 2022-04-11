import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class KeefService {
docUrl='http://sarcasticmind8-001-site1.gtempurl.com/Doctors/getall'
staticUrl:'http://sarcasticmind8-001-site1.gtempurl.com/images'
  constructor(private http : HttpClient) { }

  getAllDoctors(){
    return this.http.get(`${this.docUrl}/GetAll`)
  }
}
