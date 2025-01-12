import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTablleComponent } from './admin-tablle.component';

describe('AdminTablleComponent', () => {
  let component: AdminTablleComponent;
  let fixture: ComponentFixture<AdminTablleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminTablleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminTablleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
