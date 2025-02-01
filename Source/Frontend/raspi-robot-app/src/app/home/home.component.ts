import { Component, OnInit } from '@angular/core';
import { OperationModeService } from '../services/operation-mode/operation-mode.service';
import { forkJoin } from 'rxjs';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [NgFor, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  selectedOperationMode: string | undefined;
  operationModes: string[] | undefined;

  constructor(private operationModeService : OperationModeService) {}

  onOperationmodeChange() {
    this.operationModeService.setOperationMode(this.selectedOperationMode!);
  }
    
  ngOnInit(): void {
    forkJoin([
      this.operationModeService.getOperationModes(),
      this.operationModeService.getCurrentOperationMode()
    ]).subscribe(([operationModes, selectedOperationMode]) => {
      this.operationModes = operationModes;
      this.selectedOperationMode = selectedOperationMode;
    });
  }
}
