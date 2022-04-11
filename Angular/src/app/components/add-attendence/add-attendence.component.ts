import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RxwebValidators } from '@rxweb/reactive-form-validators';
import { Attend } from 'src/app/Models/attend';
import { AttendenceService } from 'src/app/services/attendence.service';
import { EmployeeService } from 'src/app/services/employee.service';
import { HolidaysService } from 'src/app/services/holidays.service';
import { SalarySettingsService } from 'src/app/services/salary-settings.service';
import { WeekendsService } from 'src/app/services/weekends.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-attendence',
  templateUrl: './add-attendence.component.html',
  styleUrls: ['./add-attendence.component.css'],
})
export class AddAttendenceComponent implements OnInit {
  attendences: any;
  employees: any;
  EmpName = '';
  holidays: any;
  settings: any;
  dateErrorMsg = '';
  attendErrorMsg = '';
  attValidation;
  contractErrorMsg = '';
  valid = false;
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
    public attService: AttendenceService,
    public empService: EmployeeService,
    public holService: HolidaysService,
    public setService: WeekendsService,
    private route: Router
  ) {
    this.attValidation = new FormGroup({
      empName: new FormControl('', [Validators.required, Validators.min(1)]),
      date: new FormControl('', [Validators.required]),
      checkin: new FormControl('', [
        Validators.required,
        RxwebValidators.minTime({ value: '07:00' }),
        RxwebValidators.maxTime({ value: '12:00' }),
      ]),
      checkout: new FormControl('', [
        Validators.required,
        RxwebValidators.minTime({ value: '12:00' }),
        RxwebValidators.maxTime({ value: '22:00' }),
        RxwebValidators.greaterThan({ fieldName: 'checkin' }),
      ]),
    });
  }

  ngOnInit(): void {
    this.attService
      .getAll()
      .subscribe((response) => (this.attendences = response));

    this.empService
      .getAllEmployees()
      .subscribe((res) => (this.employees = res));

    this.holService.getHolidays().subscribe((res) => (this.holidays = res));

    this.setService.getWeekend().subscribe((res) => (this.settings = res));
  }

  onSubmit() {
    this.checkContractDate();
    this.checkDayAhead();
    this.checkDaysOff();
    this.checkValid();
    if (this.valid == true && this.attService.formData.id == 0) {
      this.addAttendence();
    } else {
      this.updateAttendence();
    }
  }

  checkValid() {
    for (let i = 0; i < this.holidays.length; i++) {
      // not an official holiday
      if (
        this.attValidation.value.date.substring(0, 10) ==
        this.holidays[i].date.substring(0, 10)
      )
        break;
      else if (
        //not a week end
        new Date(this.attValidation.value.date).getDay() !=
          this.days.indexOf(this.settings[0].weekend1) &&
        new Date(this.attValidation.value.date).getDay() !=
          this.days.indexOf(this.settings[0].weekend2) &&
        // not after today
        new Date(this.attValidation.value.date) < new Date() &&
        //not before contract date
        new Date(
          this.employees[
            this.employees.findIndex(
              (i: any) => i.id == this.attValidation.value.empName
            )
          ].dateOfContract
        ) < new Date(this.attValidation.value.date)
      )
        this.valid = true;
      else this.valid = false;
    }
  }

  checkDaysOff() {
    for (let i = 0; i < this.holidays.length; i++) {
      if (
        this.attValidation.value.date.substring(0, 10) ==
          this.holidays[i].date.substring(0, 10) ||
        new Date(this.attValidation.value.date).getDay() ==
          this.days.indexOf(this.settings[0].weekend1) ||
        new Date(this.attValidation.value.date).getDay() ==
          this.days.indexOf(this.settings[0].weekend2)
      )
        this.dateErrorMsg = 'the enterd date is a day off ';
    }
  }

  checkName(){

  }

  checkContractDate() {
    if (
      new Date(
        this.employees[
          this.employees.findIndex(
            (i: any) => i.id == this.attValidation.value.empName
          )
        ].dateOfContract
      ) >= new Date(this.attValidation.value.date)
    ) {
      this.contractErrorMsg =
        "the entered day is behind employee's contract date";
    } else this.contractErrorMsg = '';
  }

  checkDayAhead() {
    if (new Date(this.attValidation.value.date) > new Date()) {
      this.attendErrorMsg = "the entered day hasn't come yet";
    } else this.attendErrorMsg = '';
  }
  addAttendence() {
    Swal.fire('Added', 'تم تسجيل الحضور بنجاح', 'success');
    this.attService.add().subscribe((res) => {
      this.route.navigate(['/']);
      this.resetForm();
    });
  }

  updateAttendence() {
    Swal.fire('Done', 'تم التعديل بنجاح', 'success');
    this.attService.put().subscribe((res) => {
      this.route.navigate(['/']);
      this.resetForm();
    });
  }

  resetForm() {
    this.attService.formData = new Attend();
  }
}

// --------------------------------- comments ----------------------------------------------//

// fun(){
//   console.log(this.employees.toArray().map((e:any) => e.id).indexOf(this.attValidation.value.empName));
// }
// console.log(this.employees.findIndex((i:any) => i.id == this.attValidation.value.empName ));
// console.log(new Date(this.employees[this.employees.findIndex((i:any) => i.id == this.attValidation.value.empName )].dateOfContract)
// <= new Date(this.attValidation.value.date)
// );
//  console.log(new Date(this.attValidation.value.date));
// console.log((new Date(this.attValidation.value.date)) > (new Date()));
// console.log(this.attValidation.value.date.substring(0,10));
// console.log(this.holidays[0].date.substring(0,10));
// console.log(this.holidays[0].date.substring(0,10) ==this.attValidation.value.date.substring(0,10));
// console.log((new Date(this.attValidation.value.date)).getDay());
// console.log(this.days.indexOf(this.settings[0].weekend1));
// console.log(this.attValidation.value.date.substring(0, 10) != this.holidays[0].date.substring(0, 10));
// console.log(   new Date(this.attValidation.value.date).getDay() != this.days.indexOf(this.settings[0].weekend1));
// console.log(    new Date(this.attValidation.value.date).getDay() !=this.days.indexOf(this.settings[0].weekend2));
// console.log(new Date(this.attValidation.value.date) < new Date());
// console.log(        new Date(
//           this.employees[
//             this.employees.findIndex(
//               (i: any) => i.id == this.attValidation.value.empName
//             )
//           ].dateOfContract
//         ) <= new Date(this.attValidation.value.date));
