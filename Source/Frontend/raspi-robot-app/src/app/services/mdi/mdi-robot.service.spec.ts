import { TestBed } from '@angular/core/testing';

import { MdiRobotService } from './mdi-robot.service';

describe('MdiRobotService', () => {
  let service: MdiRobotService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MdiRobotService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
