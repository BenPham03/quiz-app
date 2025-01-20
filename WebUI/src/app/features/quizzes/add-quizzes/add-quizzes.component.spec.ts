import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddQuizzesComponent } from './add-quizzes.component';

describe('AddQuizzesComponent', () => {
  let component: AddQuizzesComponent;
  let fixture: ComponentFixture<AddQuizzesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddQuizzesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddQuizzesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
