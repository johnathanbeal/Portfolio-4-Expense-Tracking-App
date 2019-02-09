import { Component, Input, OnInit } from '@angular/core';
import { Expense } from './model';
import { ExpenseQueryService } from './ExpenseQuery.Service';
import { Observable } from 'rxjs';
import { ExpenseDeletionService } from './expenseDeletion.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'YNAET: You Need An Expense Tracker';


  expenses: Expense[];
  expense: Expense = new Expense();


  hideExpense = true;

  categories   = ['Movies', 'Pokemon', 'Groceries', 'Jujitsu', 'Mortgage', 'Cell', 'Cable', 'Other'];

  /* Create an array of states that includes previous states PLUS Illinois */
  colorCode    = ['Grey', 'Black', 'Red', 'Orange', 'Yellow', 'Green', 'Blue', 'Purple'];

  account = ['Wachovia', 'Middleburg Bank', 'HSBC', 'First Interstate']

  constructor(private expenseQueryService: ExpenseQueryService, private expenseDeletionService: ExpenseDeletionService) { }

  ngOnInit() {
    this.expenseQueryService.getAllExpenses()
      .subscribe(xp => this.expenses = xp);
        
  }

  deleteExpense() {
    this.expenseDeletionService.deleteExpense(this.expense.id).subscribe(
      () => console.log('Expense with Id = {{this.expense}} deleted'),
        (err) => console.log(err)
    );
    var index = this.expenses.indexOf(this.expense);
    this.expenses.splice(index, 1);
  }

  clearExpenses() {
    this.expense.amount = 0;
    this.expense.payee = "";
    this.expense.memo = "";
    this.expense.category = "";
    this.expense.account = "";
    this.expense.date = "";
    this.expense.repeat = null;
    this.expense.impulse = null;
    this.expense.colorCode = "";
    this.expense.id = 0;
  }
}
