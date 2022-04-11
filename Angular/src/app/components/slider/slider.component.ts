import {
  AfterViewInit,
  Component,
  ElementRef,
  QueryList,
  ViewChildren,
} from '@angular/core';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css'],
})

//using afterViewInit interface to load all components first
export class SliderComponent implements AfterViewInit {
  constructor() {}
  i = 0;
  @ViewChildren('img')
  public listItems!: QueryList<ElementRef<HTMLLIElement>>;

  public ngAfterViewInit() {}

  next() {
    if (
      this.listItems.toArray()[this.i].nativeElement.className == 'active' &&
      this.i < 2
    ) {
      this.listItems.forEach((e) => (e.nativeElement.className = 'inactive'));
      this.listItems.toArray()[this.i + 1].nativeElement.className = 'active';
      this.i++;
    }
  }

  previous() {
    if (
      this.listItems.toArray()[this.i].nativeElement.className == 'active' &&
      this.i > 0
    ) {
      this.listItems.forEach((e) => (e.nativeElement.className = 'inactive'));
      this.listItems.toArray()[this.i - 1].nativeElement.className = 'active';
      this.i--;
    }
  }
}
