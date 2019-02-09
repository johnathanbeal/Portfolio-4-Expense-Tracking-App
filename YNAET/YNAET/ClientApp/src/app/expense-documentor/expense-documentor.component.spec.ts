import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseDocumentorComponent } from './expense-documentor.component';

describe('ExpenseDocumentorComponent', () => {
  let component: ExpenseDocumentorComponent;
  let fixture: ComponentFixture<ExpenseDocumentorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpenseDocumentorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseDocumentorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
