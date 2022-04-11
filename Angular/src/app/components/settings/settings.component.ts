import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SalarySettingsService } from 'src/app/services/salary-settings.service';
import { WeekendsService } from 'src/app/services/weekends.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
})
export class SettingsComponent implements OnInit {
  days: string[] = [
    'sunday',
    'monday',
    'tuesday',
    'wednesday',
    'thursday',
    'friday',
    'saturday',
  ];
  weekends: any;
  salSettings: any;
  setValidation;
  uniqueMsg = '';
  constructor(
    public weekService: WeekendsService,
    public salService: SalarySettingsService
  ) {
    this.setValidation = new FormGroup({
      extrahour: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(5),
      ]),
      penalityhour: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.max(5),
      ]),
      weekend1: new FormControl('', [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.weekService.getWeekend().subscribe((res) => (this.weekends = res));
    this.salService
      .getSalSettings()
      .subscribe((res) => (this.salSettings = res));
  }
  onSubmit() {
    if (
      this.setValidation.value.weekend1 != this.setValidation.value.weekend2 &&
      this.setValidation.status == 'VALID'
    ) {
      this.addWeekend();
      this.addSalSettings();
    } else {
      this.uniqueMsg = 'Choose different days';
    }
  }

  addWeekend() {
    this.weekService.postWeekend().subscribe((res) => {
      Swal.fire('Added', 'Settings added successfully', 'success');
      this.resetForm();
    });
  }
  addSalSettings() {
    this.salService.postSalSettings().subscribe((res) => console.log('done'));
  }

  resetForm() {
    this.weekService.formData.weekend1 = '';
    this.weekService.formData.weekend2 = '';
    this.salService.formData.penalityhour = '';
    this.salService.formData.extrahour = '';
  }
}
