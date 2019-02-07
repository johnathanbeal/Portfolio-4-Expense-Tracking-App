import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseData {
  id: string;
}

@Injectable()
export class ExpenseDataService {
  constructor(private http: HttpClient) { }

  getAllExpenses(): Observable<Expense[]> {
    return this.http.get<Expense[]>('https://localhost:44354/api/expenses');
  }

  insertExpense(expenseData: ExpenseData): Observable<Expense> {
    return this.http.post<Expense>('http://localhost:44354/api/expenses/', expenseData.id);
  }

  updateExpense(expenseData: ExpenseData): Observable<void> {
    return this.http.put<void>('http://localhost:44354/api/expenses' + expenseData.id, expenseData);
  }


}
