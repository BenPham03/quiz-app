import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatePasswordModalComponent } from './update-password-modal.component';

describe('UpdatePasswordModalComponent', () => {
  let component: UpdatePasswordModalComponent;
  let fixture: ComponentFixture<UpdatePasswordModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdatePasswordModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdatePasswordModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
