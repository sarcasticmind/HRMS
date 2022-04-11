import { Component, OnInit } from '@angular/core';
import { Attend } from 'src/app/Models/attend';
import { AttendenceService } from 'src/app/services/attendence.service';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-attendence-details',
  templateUrl: './attendence-details.component.html',
  styleUrls: ['./attendence-details.component.css'],
})
export class AttendenceDetailsComponent implements OnInit {
  start: string;
  end: string;
  empName: string;
  attendences: any;
  employees: any;
  constructor(
    public attService: AttendenceService,
    public empService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.empService
      .getAllEmployees()
      .subscribe((res) => (this.employees = res));
  }

  show() {
    this.attService
      .getFiltered(this.start, this.end, this.empName)
      .subscribe((res) => (this.attendences = res));
  }

  getData(selected: Attend) {
    this.attService.formData = selected;
  }
}
