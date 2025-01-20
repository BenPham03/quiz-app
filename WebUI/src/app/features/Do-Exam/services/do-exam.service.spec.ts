import { TestBed } from '@angular/core/testing';

import { DoExamService } from './do-exam.service';

describe('DoExamService', () => {
  let service: DoExamService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoExamService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
