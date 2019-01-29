import { Component, Input } from '@angular/core';
import { Expense } from './model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'YNAET: You Need An Expense Tracker';

  expenses: Expense[] = [
    {
      id: 1,
	    amount: 70.00,
	    payee: 'Alamo Drafthouse',
	    category: 'Movies',
	    account: 'Wachovia',
	    date: '20190125',
	    repeat: false,
	    memo: 'Into the Spiderverse Tickets',
	    impulse: true,
	    colorCode: 'Red'
    },
    {
      id: 2,
	    amount: 10.00,
	    payee: 'Pokemon Trainer Kit',
	    category: 'Pokemon',
	    account: 'Middleburg Bank',
	    date: '20190126',
	    repeat: false,
	    memo: 'Sun and Moon Trainer Cards',
	    impulse: true,
	    colorCode: 'Orange'
    },
    {
      id: 3,
	    amount: 190.00,
	    payee: 'Google Fi',
	    category: 'Cell',
	    account: 'HSBC',
	    date: '20190127',
	    repeat: true,
	    memo: 'Into the Spiderverse Tickets',
	    impulse: false,
	    colorCode: 'Green'
    },
    {
      id: 4,
	    amount: 200.00,
	    payee: 'Groceries',
	    category: 'Groceries',
	    account: 'First Interstate',
	    date: '20190128',
	    repeat: false,
	    memo: 'Lidl',
	    impulse: false,
	    colorCode: 'Blue'
    },
  ];

  expense: Expense = new Expense();

  hideExpense = true;

  categories   = ['Movies', 'Pokemon', 'Groceries', 'Jujitsu', 'Mortgage', 'Cell', 'Cable', 'Other'];

  /* Create an array of states that includes previous states PLUS Illinois */
  colorCode    = ['Grey', 'Black', 'Red', 'Orange', 'Yellow', 'Green', 'Blue', 'Purple'];

  account = ['Wachovia', 'Middleburg Bank', 'HSBC', 'First Interstate']

}
