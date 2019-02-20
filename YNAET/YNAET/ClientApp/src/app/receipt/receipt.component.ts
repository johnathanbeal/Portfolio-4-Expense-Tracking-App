import { Component, OnInit } from '@angular/core';
import { Expense } from '../model';
import { SharedDataService } from '../sharedData.service';
import { ExpenseQueryService } from '../ExpenseQuery.Service';
import { ExpenseDeletionService } from '../expenseDeletion.service';
import { ExpenseModificationService } from '../expenseModification.service';


@Component({
  selector: 'receipts',
  templateUrl: './receipt.component.html',
  styleUrls: ['./receipt.component.css']
})
export class ReceiptComponent implements OnInit {

  hideExpense = true;

  expenses: Expense[];
  expense: Expense = new Expense();

  accounts: string[];
  account: string;

  colorCodes: string[];
  colorCode: string;

  categories: string[];
  category: string;

  constructor(private expenseQueryService: ExpenseQueryService,
    private expenseDeletionService: ExpenseDeletionService,
    private expenseModificationService: ExpenseModificationService,
    private sharedDataService: SharedDataService
) { }

  ngOnInit() {
    this.expenseQueryService.getAllExpenses()
      .subscribe(xp =>
      {
        this.expenses = xp
        this.expense = this.expenses[0];
      });

    

    this.accounts = this.sharedDataService.getAccounts();
    this.colorCodes = this.sharedDataService.getColorCodes();
    this.categories = this.sharedDataService.getCategories();
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
    this.expense.id = 0;
    this.expense.amount = 0.00;
    this.expense.payee = "";
    this.expense.date = "";
    this.expense.category = "";
    this.expense.repeat = null;
    this.expense.impulse = null;
    this.expense.account = "";
    this.expense.colorCode = "";
  }

}
