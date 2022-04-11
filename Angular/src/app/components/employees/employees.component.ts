import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/Models/employee';
import { EmployeeService } from 'src/app/services/employee.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent implements OnInit {
  employees: any;
  constructor(public myService: EmployeeService) {}

  ngOnInit(): void {
    this.myService.getAllEmployees().subscribe((response) => {
      this.employees = response;
    });
  }
  delete(id: number) {
    Swal.fire({
      title: 'Remove this employee?',
      text: 'This employee will be removed forever ',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, I am sure',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      console.log(result);
      if (result.value) {
        this.myService.deleteEmployee(id).subscribe(
          (res) => {
            window.location.reload();
          },
          (err) => {
            console.log(err);
          }
        );
        Swal.fire('Deleted', 'The employee has been deleted', 'success');
      }
    });
  }

  getData(selected: Employee) {
    this.myService.formData = selected;
  }
}
