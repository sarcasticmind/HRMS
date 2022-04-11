import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  NumericValueType,
  RxwebValidators,
} from '@rxweb/reactive-form-validators';
import { Employee } from 'src/app/Models/employee';
import { EmployeeService } from 'src/app/services/employee.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent implements OnInit {
  employees: any;
  nameErrorMsg=''
  unique = false;
  empValidation: any;
  contractErrorMsg = '';
  constructor(public empService: EmployeeService, private route: Router) {
    this.empValidation = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        RxwebValidators.pattern({
          expression: { 'invalid name': /^[A-Za-z]+$/ },
        }),
      ]),
      address: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
      ]),
      nationality: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        RxwebValidators.pattern({
          expression: { 'invalid name': /^[A-Za-z]+$/ },
        }),
      ]),
      nationalId: new FormControl('', [
        Validators.required,
        RxwebValidators.pattern({
          expression: { invalid: /^[2-3][0-9]{13}$/ },
        }),
      ]),
      phone: new FormControl('', [
        Validators.required,
        RxwebValidators.pattern({
          expression: { invalid: /^01[0|1|2|5][0-9]{8}$/ },
        }),
      ]),
      gender: new FormControl('', [Validators.required]),
      contract: new FormControl('', [
        Validators.required,
        RxwebValidators.minDate({ value: '2008-01-01', allowISODate: true }),
      ]),
      salary: new FormControl('', [
        Validators.required,
        RxwebValidators.numeric({
          message: 'not a valid number',
          allowDecimal: true,
          acceptValue: NumericValueType.Both,
        }),
        Validators.min(6000),
      ]),

      dob: new FormControl('', [Validators.required]),
      checkin: new FormControl('', [
        Validators.required,
        RxwebValidators.minTime({ value: '07:00' }),
        RxwebValidators.maxTime({ value: '12:00' }),
      ]),
      checkout: new FormControl('', [
        Validators.required,
        RxwebValidators.minTime({ value: '12:00' }),
        RxwebValidators.maxTime({ value: '20:00' }),
        RxwebValidators.greaterThan({ fieldName: 'checkin' }),
      ]),
    });
  }

  ngOnInit(): void {
    this.empService
      .getAllEmployees()
      .subscribe((res) => (this.employees = res));
  }

  onSubmit() {
    this.checkName();
    this.checkPhone();
    if (
      new Date(this.empValidation.value.dob).getFullYear() >
      new Date(this.empValidation.value.contract).getFullYear() - 20
    ) {
      this.contractErrorMsg = 'age must be more than 20 years old';
    }
    if (
      this.empService.formData.id == 0 &&
      new Date(this.empValidation.value.dob).getFullYear() <=
        new Date(this.empValidation.value.contract).getFullYear() - 20 &&
        this.unique==true &&
        this.uniquePhone==true
    ) {
      this.addEmp();
    } else {
      this.putEmp();
    }
  }

  checkName(){
    for (let i = 0; i < this.employees.length; i++) {
      //check for if the email exists already
      if (this.empValidation.value.name == this.employees[i].name) {
        this.unique = false;
        this.nameErrorMsg = 'error : name used before';
        break;
      } else this.unique = true;
    }
  }
  uniquePhone=false;
  phoneErrorMsg=''
  checkPhone(){
    for (let i = 0; i < this.employees.length; i++) {
      //check for if the email exists already
      if (this.empValidation.value.phone == this.employees[i].phoneNumber) {
        this.uniquePhone = false;
        this.phoneErrorMsg = 'error : phone used before';
        break;
      } else this.uniquePhone = true;
    }
  }
  addEmp() {
    this.empService.addEmployee().subscribe((res) => {
      Swal.fire('Added', 'The emloyee has been added successfully', 'success');
      this.route.navigate(['/employee']);
      this.resetForm();
    });
  }

  putEmp() {
    this.empService.updateEmployee().subscribe((res) => {
      Swal.fire(
        'Done',
        'This emloyee has been edited  successfully',
        'success'
      );
      this.route.navigate(['/employee']);
      this.resetForm();
    });
  }
  resetForm() {
    this.empService.formData = new Employee();
  }
}
