import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Attend } from 'src/app/Models/attend';
import { AttendenceService } from 'src/app/services/attendence.service';
import { EmployeeService } from 'src/app/services/employee.service';
import { WeekendsService } from 'src/app/services/weekends.service';

@Component({
  selector: 'app-attendence',
  templateUrl: './attendence.component.html',
  styleUrls: ['./attendence.component.css'],
})
export class AttendenceComponent implements OnInit {
  counter = 0;
  attendences: any;
  employees: any;
  i = 0;
  dateInput: string = '';
  holiday: any;
  weekends: any;
  days: string[] = [
    'sunday',
    'monday',
    'tuesday',
    'wednesday',
    'thursday',
    'friday',
    'saturday',
  ];

  constructor(
    public myService: AttendenceService,
    public empService: EmployeeService,
    public weekService: WeekendsService,
    private route: ActivatedRoute
  ) {}
  month: number;
  year: number;
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
  EmpName = '';
  order: string;
  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.month = params['month'];
      this.year = params['year'];
      this.EmpName = params['empName'];
    });

    this.myService.getAll().subscribe((response) => {
      this.attendences = response;
    });
    this.empService
      .getAllEmployees()
      .subscribe((res) => (this.employees = res));

    this.myService.getHolidays().subscribe((res) => (this.holiday = res));
    this.weekService.getWeekend().subscribe((res) => (this.weekends = res));
  }

  check() {
    this.counter = 0;

    console.log(this.attendences[this.i].day.substring(5, 7));

    for (let i = 0; i < this.attendences.length; i++) {
      if (
        this.attendences[i].attendingTime == null &&
        this.attendences[i].emp.name == this.EmpName &&
        new Date(this.attendences[i].day).getMonth() ==
          new Date(this.dateInput).getMonth() &&
        this.holiday[0].date != this.attendences[i].day &&
        this.days[new Date(this.attendences[i].day).getDay()] !=
          this.weekends[0].weekend1 &&
        this.days[new Date(this.attendences[i].day).getDay()] !=
          this.weekends[0].weekend2
      ) {
        this.counter++;
      }
    }
  }

  getData(selected: Attend) {
    this.myService.formData = selected;
  }

  print(){
    window.print()
  }
}
