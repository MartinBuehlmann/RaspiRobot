import { TestBed } from '@angular/core/testing';

import { OperationModeService } from './operation-mode.service';

describe('OperationModeService', () => {
  let service: OperationModeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OperationModeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
