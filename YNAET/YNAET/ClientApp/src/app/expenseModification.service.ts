import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseModifier {
  id: string;
}

@Injectable()
export class ExpenseModificationService {
  constructor(private http: HttpClient) { }
  
  updateExpense(expenseModifier: ExpenseModifier): Observable<void> {
    return this.http.put<void>('https://localhost:44354/api/expenses/' + expenseModifier.id, expenseModifier);
  }
  
}
