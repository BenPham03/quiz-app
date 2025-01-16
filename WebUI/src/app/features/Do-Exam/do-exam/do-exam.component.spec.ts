import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoExamComponent } from './do-exam.component';

describe('DoExamComponent', () => {
  let component: DoExamComponent;
  let fixture: ComponentFixture<DoExamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoExamComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
