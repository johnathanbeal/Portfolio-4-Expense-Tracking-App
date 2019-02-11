import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseCreator {
  expense: Expense;
}

@Injectable()
export class ExpenseCreationService {
  constructor(private http: HttpClient) { }
  
  insertExpense(expense: Expense): Observable<Expense> {
      return this.http.post<Expense>('https://localhost:44354/api/expenses/', expense);
  }
}
