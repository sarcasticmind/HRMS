import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { EmployeeService } from 'src/app/services/employee.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-salary-report',
  templateUrl: './salary-report.component.html',
  styleUrls: ['./salary-report.component.css'],
})
export class SalaryReportComponent implements OnInit {
  year: number;
  month: number;
  empName:string;
  reports: any;
  employees:any;
  months = [
    '',
    'january',
    'Febraury',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December',
  ];
  constructor(public salService: ReportService ,
    public empService:EmployeeService,
    public router : Router) {}

  ngOnInit(): void {
    this.empService.getAllEmployees().subscribe(
      res => this.employees = res
    );
    // this.salService
    // .getAll(2022, 2)
    // .subscribe((res) => (this.reports = res));
  }


  calc(){
    this.salService
    .getAll(this.year, this.month)
    .subscribe((res) => (this.reports = res));
  }

  goAttend(name:string){
      this.router.navigate(
        ['/attend'],
        {queryParams:{month: `${this.month}`, 'year': `${this.year}` , 'empName' :`${name}`}}
      );
  }

  getData(month:number , year:number , name:string){
    this.month = month;
    this.year = year;
    this.empName = name;
  }

  print(){
    window.print()
  }
}
