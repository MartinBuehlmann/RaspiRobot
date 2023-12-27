import { TestBed } from '@angular/core/testing';

import { OperationModeChangedService } from './operation-mode-changed.service';

describe('OperationModeChangedServiceService', () => {
  let service: OperationModeChangedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OperationModeChangedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
