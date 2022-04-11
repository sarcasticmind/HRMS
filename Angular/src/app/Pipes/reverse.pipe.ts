import { Pipe } from "@angular/core";

@Pipe({
  name: 'reverse'
})
export class ReversePipe {
  transform(arr:any) {
    var copy = arr.slice();
    return copy.reverse();
  }
}
