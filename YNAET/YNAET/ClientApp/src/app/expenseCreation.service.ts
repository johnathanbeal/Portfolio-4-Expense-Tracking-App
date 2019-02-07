import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseCreator {
  id: string;
}

@Injectable()
export class ExpenseCreationService {
  constructor(private http: HttpClient) { }
  
  insertExpense(expenseCreator: ExpenseCreator): Observable<Expense> {
    return this.http.post<Expense>('http://localhost:44354/api/expenses/', expenseCreator.id);
  }
}
