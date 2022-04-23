import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogExportOrderComponent } from './dialog-export-order.component';

describe('DialogExportOrderComponent', () => {
  let component: DialogExportOrderComponent;
  let fixture: ComponentFixture<DialogExportOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogExportOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogExportOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
