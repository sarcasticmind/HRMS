import { Injectable } from '@angular/core';
import {  Observable, Subject } from 'rxjs';
@Injectable()
export class CounterService {

  private siblingCounter= new Subject<number>();
  constructor() { }

  // Get the counter value
  public getCounter(): Observable<number> {
    return this.siblingCounter.asObservable();
  }
// Updating the cunter value
  public updateCounter(counter: number): void {
    this.siblingCounter.next(counter);
  }

}
