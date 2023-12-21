import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationModeComponent } from './operation-mode.component';

describe('OperationModeComponent', () => {
  let component: OperationModeComponent;
  let fixture: ComponentFixture<OperationModeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OperationModeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OperationModeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
