import { Component, OnInit } from '@angular/core';
import { HolidaysService } from 'src/app/services/holidays.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.css'],
})
export class HolidaysComponent implements OnInit {
  holidays: any;
  constructor(public holyService: HolidaysService) {}

  ngOnInit(): void {
    this.holyService.getHolidays().subscribe((res) => (this.holidays = res));
  }

  delete(id: number) {
    Swal.fire({
      title: 'Remove this holiday?',
      text: 'This holiday will be removed forever ',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, I am sure',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      console.log(result);
      if (result.value) {
        this.holyService.deleteHoliday(id).subscribe((res) => {
          window.location.reload();
        });
      }
    });
  }

  getData(selected: { id: 0; name: ''; date: '' }) {
    this.holyService.formData = selected;
  }
}
