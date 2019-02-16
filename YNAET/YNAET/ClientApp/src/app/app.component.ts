import { Component, Input, OnInit } from '@angular/core';
import { Expense } from './model';
import { ExpenseQueryService } from './ExpenseQuery.Service';
import { Observable } from 'rxjs';
import { ExpenseDeletionService } from './expenseDeletion.service';
import { ExpenseCreationService } from './expenseCreation.service';
import { Router } from '@angular/router';
import { ExpenseModificationService } from './expenseModification.service';


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

  accounts = ['Suntrust', 'Middleburg', 'Wells Fargo'];

  colorCodes = ['Grey', 'Black', 'Red', 'Orange', 'Yellow', 'Green', 'Blue', 'Purple', 'Pink', 'Cornflower-Blue'];

  categories = ['Rebalance', 'Tax Return', 'Offering', 'Cell', 'Utilities',
    'Mortgage', 'Groceries', 'Car Gas', 'Trips', 'Birthdays', 'Celebrations',
    'Christmas', 'Kittens', 'Preschool', 'Car Expenses', 'EZ Pass', 'Subscriptions',
    'Stuff I Forgot to Budget For', 'Auto Loan', 'Student Loan', 'Jujitsu/Krav Maga',
    'Swimming', 'VA529', 'Training Fund', 'Sports Gym', 'Dining Out', 'Fun Money']

  constructor(
    private expenseQueryService: ExpenseQueryService,
    private expenseDeletionService: ExpenseDeletionService,
    private expenseCreationService: ExpenseCreationService,
    private expenseModificationService: ExpenseModificationService)
  //private router: Router)
  { }

  ngOnInit() {
    this.expenseQueryService.getAllExpenses()
      .subscribe(xp => this.expenses = xp);       
  }

  createExpense(expense) {
    this.expenseCreationService.insertExpense(expense).subscribe();
  }

  deleteExpense() {
    this.expenseDeletionService.deleteExpense(this.expense.id).subscribe(
      () => console.log('Expense with Id = {{this.expense}} deleted'),
        (err) => console.log(err)
    );
    var index = this.expenses.indexOf(this.expense);
    this.expenses.splice(index, 1);
  }

  updateExpense() {
    this.expenseModificationService.updateExpense(this.expense).subscribe();
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
