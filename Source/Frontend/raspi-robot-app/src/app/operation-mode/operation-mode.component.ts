import { Component, OnInit } from '@angular/core';
import { OperationModeService } from '../services/operation-mode/operation-mode.service';
import { OperationModeChangedService } from '../services/operation-mode/operation-mode-changed.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-operation-mode',
  imports: [NgIf],
  templateUrl: './operation-mode.component.html',
  styleUrl: './operation-mode.component.scss'
})
export class OperationModeComponent implements OnInit {
  operationMode: string | undefined;
  
  constructor(
    private operationModeService : OperationModeService,
    private operationModeChangedService : OperationModeChangedService) {}

  ngOnInit(): void {
    this.operationModeService.getCurrentOperationMode()
    .subscribe(
      (currentOperationMode: string) => {
        this.operationMode = currentOperationMode;
      },
      (error) => console.error('Error fetching string: ', error)
    );
    
    this.operationModeChangedService
      .updateOperationMode(
        operationMode => this.operationMode = operationMode);
  }
}
