import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AttendenceService } from 'src/app/services/attendence.service';


@Component({
  selector: 'app-add-product',
  styleUrls: ['./add-product.component.css'],
  templateUrl: './add-product.component.html'
})
export class AddProductComponent implements OnInit {
  attendences:any;
  count=5

  constructor( private attService: AttendenceService) {

   }
  ngOnInit(): void {
    this.attService.getAll().subscribe((response) => {
      this.attendences = response;
    });
  }

}
