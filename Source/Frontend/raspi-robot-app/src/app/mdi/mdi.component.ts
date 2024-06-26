import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MdiRobotService } from '../services/mdi/mdi-robot.service';
import { PositionModel } from '../services/robot/position-model';
import { RobotService } from '../services/robot/robot.service';
import { AxisDirection } from '../services/mdi/axis-direction';
import { RobotAxisPositionChangedService } from '../services/robot/robot-axis-position-changed.service';

@Component({
  selector: 'app-mdi',
  templateUrl: './mdi.component.html',
  styleUrl: './mdi.component.scss'
})
export class MdiComponent implements OnInit {
  axisPositions : number[] = [0, 0, 0, 0, 0, 0];

  constructor(
    private mdiRobotService : MdiRobotService,
    private robotService : RobotService,
    private robotAxisChangedService : RobotAxisPositionChangedService,
    private changeDetection : ChangeDetectorRef) {}

  ngOnInit(): void {
    this.updateRobotAxisPositions();
    
    this.robotAxisChangedService
      .robotAxisPositionChanged(
        () => this.updateRobotAxisPositions());
  }

  updateRobotAxisPositions(): void {
    this.robotService.getAllAxisCurrentPositions()
    .subscribe((axisPositions: PositionModel[]) => {
      axisPositions
        .forEach(axisPosition => 
          this.axisPositions[axisPosition.axis] = axisPosition.position);
    });
  }

  stepPlus(axisNumber: number) {
    this.step(axisNumber, AxisDirection.Plus, 1);
  }

  stepPlusFast(axisNumber: number) {
    this.step(axisNumber, AxisDirection.Plus, 5);
  }

  stepMinus(axisNumber: number) {
    this.step(axisNumber, AxisDirection.Minus, 1);
  }

  stepMinusFast(axisNumber: number) {
    this.step(axisNumber, AxisDirection.Minus, 5);
  }

  step(axisNumber: number, axisDirection: AxisDirection, stepSize: number) {
    this.mdiRobotService.stepSingleAxis(axisNumber, axisDirection, stepSize)
    .subscribe(response => {
      if (response.executed) {
        this.axisPositions[response.newPosition.axis] = response.newPosition.position;
        this.changeDetection.detectChanges();
      }
    });
  }
}
