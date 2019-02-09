import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseQuery {
  id: string;
}

@Injectable()
export class ExpenseQueryService {
  constructor(private http: HttpClient) { }

  getAllExpenses(): Observable<Expense[]> {
    return this.http.get<Expense[]>('https://localhost:44354/api/expenses');
  }

}
