import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RobotService } from '../../services/robot/robot.service';
import { RobotAxisPositionChangedService } from '../../services/robot/robot-axis-position-changed.service';
import { PositionModel } from '../../services/mdi/position-model';

@Component({
  selector: 'app-robot',
  templateUrl: './robot.component.html',
  styleUrl: './robot.component.scss'
})
export class RobotComponent implements OnInit {
  axisPositions : number[] = [0, 0, 0, 0, 0, 0];

  constructor(
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
}
