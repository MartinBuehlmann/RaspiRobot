import { Component, OnInit } from '@angular/core';
import { OperationModeService } from '../services/operation-mode/operation-mode.service';

@Component({
  selector: 'app-operation-mode',
  templateUrl: './operation-mode.component.html',
  styleUrl: './operation-mode.component.scss'
})
export class OperationModeComponent implements OnInit {
  operationMode: string | undefined;
  
  constructor(private operationModeService : OperationModeService) {}

  ngOnInit() {
    this.operationModeService.getCurrentOperationMode()
    .subscribe(
      (currentOperationMode: string) => {
        this.operationMode = currentOperationMode;
      },
      (error) => console.error('Error fetching string: ', error)
    )
  }
}
