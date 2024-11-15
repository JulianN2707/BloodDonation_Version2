import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonationRegistrationComponent } from './donation-registration.component';

describe('DonationRegistrationComponent', () => {
  let component: DonationRegistrationComponent;
  let fixture: ComponentFixture<DonationRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonationRegistrationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DonationRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
