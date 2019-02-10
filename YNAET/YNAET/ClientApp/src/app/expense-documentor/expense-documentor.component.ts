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

  expenseForm: FormGroup;

  accounts = ['Suntrust', 'Middleburg', 'Wells Fargo'];

  colorCodes = ['Grey', 'Black', 'Red', 'Orange', 'Yellow', 'Green', 'Blue', 'Purple', 'Pink', 'Cornflower-Blue'];

  categories = ['Rebalance', 'Tax Return', 'Offering', 'Cell', 'Utilities',
    'Mortgage', 'Groceries', 'Car Gas', 'Trips', 'Birthdays', 'Celebrations',
    'Christmas', 'Kittens', 'Preschool', 'Car Expenses', 'EZ Pass', 'Subscriptions',
    'Stuff I Forgot to Budget For', 'Auto Loan', 'Student Loan', 'Jujitsu/Krav Maga',
    'Swimming', 'VA529', 'Training Fund', 'Sports Gym', 'Dining Out', 'Fund Money']

  constructor() {
    this.expenseForm = this.createFormGroup();
  }

  createFormGroup() {
    return new FormGroup({
      amount: new FormControl('amount'),
      payee: new FormControl('payee'),
      category: new FormControl('category'),
      account: new FormControl('account'),
      date: new FormControl('date'),
      repeat: new FormControl('repeat'),
      memo: new FormControl('memo'),
      impulse: new FormControl('impulse'),
      colorCode: new FormControl('color'),
    });
  }

  ngOnInit() {
  }
}
