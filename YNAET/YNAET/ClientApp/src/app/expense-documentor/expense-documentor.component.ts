import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Expense } from '../model';
import { FormBuilder } from '@angular/forms';
import { ExpenseCreationService } from '../expenseCreation.service';


@Component({
  selector: 'app-expense-documentor',
  templateUrl: './expense-documentor.component.html',
  styleUrls: ['./expense-documentor.component.css']
})



export class ExpenseDocumentorComponent implements OnInit {

  newExpense: Expense;

  expenseForm: FormGroup;

  accounts = ['--Select An Account--', 'Suntrust', 'Middleburg', 'Wells Fargo'];

  colorCodes = ['--Select a Color', 'Grey', 'Black', 'Red', 'Orange', 'Yellow', 'Green', 'Blue', 'Purple', 'Pink', 'Cornflower-Blue'];

  categories = ['--Select a Category--', 'Rebalance', 'Tax Return', 'Offering', 'Cell', 'Utilities',
    'Mortgage', 'Groceries', 'Car Gas', 'Trips', 'Birthdays', 'Celebrations',
    'Christmas', 'Kittens', 'Preschool', 'Car Expenses', 'EZ Pass', 'Subscriptions',
    'Stuff I Forgot to Budget For', 'Auto Loan', 'Student Loan', 'Jujitsu/Krav Maga',
    'Swimming', 'VA529', 'Training Fund', 'Sports Gym', 'Dining Out', 'Fun Money']

  constructor(private fb: FormBuilder, private expenseCreationService: ExpenseCreationService) {
  }

  createFormGroup() {
    this.expenseForm = this.fb.group({
      amount: [''],
      payee: [''],
      category: [''],
      account: [''],
      date: [''],
      repeat: [''],
      memo: [''],
      impulse: [''],
      colorCode: [''],
    });
  }

  onSubmit() {
    console.log('reactive', this.expenseForm.value);
    this.newExpense = this.expenseForm.value;
    this.expenseCreationService.insertExpense(this.newExpense).subscribe();
    //this.expenseForm.reset();
    //location.reload();
  }

  ngOnInit() {
    this.createFormGroup();
  }
}
