import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Expense } from './model';

export interface ExpenseDeletor {
  id: string;
}

@Injectable()
export class ExpenseDeletionService {
  constructor(private http: HttpClient) { }
  
  deleteExpense(id: number) {
    return this.http.delete('https://localhost:44354/api/expenses/' + id);
  }
}
