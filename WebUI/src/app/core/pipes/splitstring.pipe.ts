import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'splitstring'
})
export class SplitstringPipe implements PipeTransform {
  transform(value: string, maxLength: number = 10): string {
    if (typeof value !== 'string' || maxLength <= 0) {
      return '';
    }

    return value.length > maxLength ? value.slice(0, maxLength) + '...' : value;
  }
}
