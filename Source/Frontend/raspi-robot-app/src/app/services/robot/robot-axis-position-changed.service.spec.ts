import { TestBed } from '@angular/core/testing';

import { RobotAxisPositionChangedService } from './robot-axis-position-changed.service';

describe('RobotAxisPositionChangedService', () => {
  let service: RobotAxisPositionChangedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RobotAxisPositionChangedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
