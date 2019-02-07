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
  
  deleteExpense(expenseDeletor: ExpenseDeletor) {
    return this.http.delete('http://localhost:8000/api/cats/' + expenseDeletor.id);
  }
}
