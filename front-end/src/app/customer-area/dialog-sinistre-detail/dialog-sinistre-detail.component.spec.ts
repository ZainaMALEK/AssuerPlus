import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogSinistreDetailComponent } from './dialog-sinistre-detail.component';

describe('DialogSinistreDetailComponent', () => {
  let component: DialogSinistreDetailComponent;
  let fixture: ComponentFixture<DialogSinistreDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogSinistreDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DialogSinistreDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
