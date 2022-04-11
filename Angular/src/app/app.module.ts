import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AddProductComponent } from './components/dashboard/add-product.component';
import { NacbarComponent } from './components/navbar/nacbar.component';
import { CounterService } from './services/counter.service';
import { FooterComponent } from './components/footer/footer.component';
import { SliderComponent } from './components/slider/slider.component';
import { AboutComponent } from './components/about/about.component';
import { ErrorComponent } from './components/error/error.component';
import {  RxReactiveFormsModule } from "@rxweb/reactive-form-validators";
import { JsonStringPipe } from './Pipes/jsonString.pipe';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { EmployeeService } from './services/employee.service';
import { AttendenceComponent } from './components/report-details/attendence.component';
import { AttendenceService } from './services/attendence.service';
import { UniqueValuePipe } from './Pipes/uniqueValue.pipe';
import { AddAttendenceComponent } from './components/add-attendence/add-attendence.component';
import { SettingsComponent } from './components/settings/settings.component';
import { AddEmployeeComponent } from './components/add-employee/add-employee.component';
import { Employee } from './Models/employee';
import { HolidaysComponent } from './components/holidays/holidays.component';
import { AddHolidayComponent } from './components/add-holiday/add-holiday.component';
import { SalaryReportComponent } from './components/salary-report/salary-report.component';
import { AttendenceDetailsComponent } from './components/attendence-details/attendence-details.component';
import { ReversePipe } from './Pipes/reverse.pipe';
import { HolidaysService } from './services/holidays.service';
import { ReportService } from './services/report.service';
import { SalarySettingsService } from './services/salary-settings.service';
import { WeekendsService } from './services/weekends.service';
import { DoctorsComponent } from './components/doctors/doctors.component';




const routes : Routes =[

  {path:'' , component:AddProductComponent},
  { path:'about' , component : AboutComponent},
  { path:'add-employee' , component : AddEmployeeComponent},
  { path:'employee' , component : EmployeesComponent},
  { path:'attend' , component : AttendenceComponent},
  { path:'add-attend' , component : AddAttendenceComponent},
  { path:'settings' , component : SettingsComponent},
  { path:'holidays' , component : AddHolidayComponent},
  { path:'attendDetail' , component : AttendenceDetailsComponent},
  { path:'report' , component : SalaryReportComponent},
  { path:'doctor' , component : DoctorsComponent},
  { path:'**' , component : ErrorComponent},

]

@NgModule({
  declarations: [
    AppComponent,
    AddProductComponent,
    NacbarComponent,
    FooterComponent,
    SliderComponent,
    AboutComponent,
    ErrorComponent,
    JsonStringPipe,
    SidebarComponent,
    EmployeesComponent,
    AttendenceComponent,
    UniqueValuePipe,
    AddAttendenceComponent,
    SettingsComponent,
    AddEmployeeComponent,
    HolidaysComponent,
    AddHolidayComponent,
    SalaryReportComponent,
    AttendenceDetailsComponent,
    ReversePipe,
    DoctorsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    HttpClientModule,
    RxReactiveFormsModule,
  ],
  providers: [
    CounterService,
    EmployeeService,
    AttendenceService,
    HolidaysService,
    ReportService,
    SalarySettingsService,
    WeekendsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
