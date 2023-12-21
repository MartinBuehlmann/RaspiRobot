import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MdiComponent } from './mdi.component';

describe('MdiComponent', () => {
  let component: MdiComponent;
  let fixture: ComponentFixture<MdiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MdiComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MdiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
