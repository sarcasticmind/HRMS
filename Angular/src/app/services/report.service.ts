import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http : HttpClient) { }

  getAll(year:number , month:number)
  {
    return this.http.get(`${environment.baseUrl}/Report?year=${year}&month=${month}`)
  }
  getByName(year:number , month:number , id:number)
  {
    return this.http.get(`${environment.baseUrl}/Report/detail?year=${year}&month=${month}&id=${id}`)
  }

}

