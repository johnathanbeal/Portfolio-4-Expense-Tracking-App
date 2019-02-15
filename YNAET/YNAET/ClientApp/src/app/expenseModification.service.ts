import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseModifier {
  expense: Expense;
}

@Injectable()
export class ExpenseModificationService {
  constructor(private http: HttpClient) { }
  
  updateExpense(expense): Observable<void> {
    return this.http.put<void>('https://localhost:44354/api/expenses/' + expense.id, expense);
  }
  
}
