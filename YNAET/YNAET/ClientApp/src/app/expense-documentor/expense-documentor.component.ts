import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Expense } from '../model';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-expense-documentor',
  templateUrl: './expense-documentor.component.html',
  styleUrls: ['./expense-documentor.component.css']
})
export class ExpenseDocumentorComponent implements OnInit {

  expenseForm = new FormGroup ({
    amount: new FormControl('amount'),
    payee: new FormControl('payee'),
    date: new FormControl('date'),
    category: new FormControl('category'),
    repeat: new FormControl('repeat'),
    impulse: new FormControl('impulse'),
    account: new FormControl('account'),
    colorCode: new FormControl('color'),
  });

  constructor() {
    this.expense = new Expense();
  }
  expense: Expense;

  ngOnInit() {
  }

}
