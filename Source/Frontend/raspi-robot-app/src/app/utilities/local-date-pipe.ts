import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'localDate',
  standalone: true
})
export class LocalDatePipe implements PipeTransform {
  transform(value: Date | string | number): string {
    if (!value) return '';
    const date = new Date(value);
    return new Intl.DateTimeFormat("de-CH", {
      dateStyle: 'short',
      timeStyle: 'medium'
    }).format(date);
  }
}