import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IntersectionListComponent } from './intersection-list.component';

describe('IntersectionListComponent', () => {
  let component: IntersectionListComponent;
  let fixture: ComponentFixture<IntersectionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IntersectionListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IntersectionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
