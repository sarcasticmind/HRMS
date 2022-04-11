import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HolidaysService } from 'src/app/services/holidays.service';

@Component({
  selector: 'app-add-holiday',
  templateUrl: './add-holiday.component.html',
  styleUrls: ['./add-holiday.component.css'],
})
export class AddHolidayComponent implements OnInit {
  holidays: any;
  holyValidation: any;
  unique = false;
  errorMsgN = '';
  errorMsgD = '';

  constructor(public holyService: HolidaysService) {
    this.holyValidation = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(3)]),
      date: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.holyService
      .getHolidays()
      .subscribe((response) => (this.holidays = response));
  }
  onSubmit() {
    for (let i = 0; i < this.holidays.length; i++) {
      //check for if the email exists already
      if (
        this.holyValidation.value.name == this.holidays[i].name
      ) {
        this.errorMsgN = 'This name already exists';
        break;
      } else if (
        this.holyValidation.value.date == this.holidays[i].date.substring(0, 10)
      ) {
        this.errorMsgD = 'This date already exists';
        break;
      } else this.unique = true;
    }
    if (
      this.unique == true &&
      this.holyService.formData.id == 0 &&
      this.holyValidation.status == 'VALID'
    ) {
      this.addHoliday();
    } else {
      this.updateHoliday();
    }
  }

  addHoliday() {
    this.holyService.postHolidays().subscribe((res) => {
      window.location.reload();
      this.resetForm();
    });
  }
  updateHoliday() {
    this.holyService.putHoliday().subscribe((res) => {
      window.location.reload();
      this.resetForm();
    });
  }
  resetForm() {
    this.holyService.formData = { id: 0, name: '', date: '' };
  }
}
