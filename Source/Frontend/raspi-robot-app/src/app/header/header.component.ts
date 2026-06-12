import { Component, ChangeDetectionStrategy } from '@angular/core';
import { OperationModeComponent } from '../operation-mode/operation-mode.component';

@Component({
  selector: 'app-header',
  imports: [OperationModeComponent],
  templateUrl: './header.component.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
