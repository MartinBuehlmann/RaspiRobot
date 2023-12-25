import { Component, OnInit } from '@angular/core';
import { MdiRobotService } from '../services/mdi/mdi-robot.service';
import { PositionModel } from '../services/mdi/position-model';
import { RobotService } from '../services/robot/robot.service';

@Component({
  selector: 'app-mdi',
  templateUrl: './mdi.component.html',
  styleUrl: './mdi.component.scss'
})
export class MdiComponent implements OnInit {
  axisPositions : number[] = [0, 0, 0, 0, 0, 0];

  constructor(
    private mdiRobotService : MdiRobotService,
    private robotService : RobotService) {
  }

  ngOnInit(): void {
    this.robotService.getAllAxisCurrentPositions()
    .subscribe((axisPositions: PositionModel[]) => {
      axisPositions
        .forEach(axisPosition => 
          this.axisPositions[axisPosition.axis] = axisPosition.position);
    });
  }
}
