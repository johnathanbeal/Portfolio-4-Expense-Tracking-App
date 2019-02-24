import { Component, Input, OnInit } from '@angular/core';
import { ExpenseQueryService } from './ExpenseQuery.Service';
import { ExpenseDeletionService } from './expenseDeletion.service';
import { ExpenseCreationService } from './expenseCreation.service';
import { ExpenseModificationService } from './expenseModification.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'YNAET: You Need An Expense Tracker';

  //accounts = ['Suntrust', 'Middleburg', 'Wells Fargo'];

  //colorCodes = ['Grey', 'Black', 'Red', 'Orange', 'Yellow', 'Green', 'Blue', 'Purple', 'Pink', 'Cornflower-Blue'];

  //categories = ['Rebalance', 'Tax Return', 'Offering', 'Cell', 'Utilities',
  //  'Mortgage', 'Groceries', 'Car Gas', 'Trips', 'Birthdays', 'Celebrations',
  //  'Christmas', 'Kittens', 'Preschool', 'Car Expenses', 'EZ Pass', 'Subscriptions',
  //  'Stuff I Forgot to Budget For', 'Auto Loan', 'Student Loan', 'Jujitsu/Krav Maga',
  //  'Swimming', 'VA529', 'Training Fund', 'Sports Gym', 'Dining Out', 'Fun Money']

  constructor(
    private expenseQueryService: ExpenseQueryService,
    private expenseDeletionService: ExpenseDeletionService,
    private expenseCreationService: ExpenseCreationService,
    private expenseModificationService: ExpenseModificationService)
  { }

  ngOnInit() {
    
  }

  createExpense(expense) {
    this.expenseCreationService.insertExpense(expense).subscribe();
  }  
}
