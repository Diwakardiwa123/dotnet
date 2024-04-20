import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentLandingComponent } from './appointment-landing.component';

describe('AppointmentLandingComponent', () => {
  let component: AppointmentLandingComponent;
  let fixture: ComponentFixture<AppointmentLandingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AppointmentLandingComponent]
    });
    fixture = TestBed.createComponent(AppointmentLandingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
