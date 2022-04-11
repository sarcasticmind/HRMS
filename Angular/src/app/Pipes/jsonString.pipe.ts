import {Pipe , PipeTransform } from "@angular/core";

@Pipe({
  name : 'jsonstring'
})
export class JsonStringPipe implements PipeTransform {
  transform(value: Object | null) {
    if(!value) return null;
    return  Object.keys(value)
  }

}
// { "required": true }   ==> "required"

