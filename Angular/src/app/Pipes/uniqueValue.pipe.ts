import {Pipe , PipeTransform } from "@angular/core";

@Pipe({
  name : 'unique'
})
export class UniqueValuePipe implements PipeTransform {
  transform(value: Object[] | null) {
    if(!value) return null;

    let filteredArr :any;
    value.forEach((item) => {
        if(!filteredArr.includes(item)) {
            filteredArr.push(item);
        }
    })
  return filteredArr;
  }

}
// { "required": true }   ==> "required"

