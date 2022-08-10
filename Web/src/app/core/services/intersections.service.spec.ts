import { TestBed } from '@angular/core/testing';

import { IntersectionsService } from './intersections.service';

describe('IntersectionsService', () => {
  let service: IntersectionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IntersectionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
