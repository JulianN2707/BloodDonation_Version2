import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrativeTableComponent } from './administrative-table.component';

describe('AdministrativeTableComponent', () => {
  let component: AdministrativeTableComponent;
  let fixture: ComponentFixture<AdministrativeTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministrativeTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdministrativeTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
