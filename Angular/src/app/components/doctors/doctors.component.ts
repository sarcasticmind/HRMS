import { Component, OnInit } from '@angular/core';
import { KeefService } from 'src/app/services/keef.service';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
doctors:any;
url= 'http://sarcasticmind8-001-site1.gtempurl.com/images';
  constructor(public docService : KeefService) {
  }
  ngOnInit(): void {
    this.docService.getAllDoctors().subscribe(
      res => this.doctors =res
    )
    // this.url = this.docService.staticUrl;
  }

  show()
  {
    console.log(this.doctors);
  }
}
