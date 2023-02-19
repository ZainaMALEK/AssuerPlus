import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclareAccidentComponent } from './declare-accident.component';

describe('DeclareAccidentComponent', () => {
  let component: DeclareAccidentComponent;
  let fixture: ComponentFixture<DeclareAccidentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeclareAccidentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeclareAccidentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
