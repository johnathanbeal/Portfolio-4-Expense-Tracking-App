import { Component, OnInit } from '@angular/core';
import { Expense } from '../model';
//import { SharedDataService } from '../sharedData.service';

@Component({
  selector: 'receipts',
  templateUrl: './receipt.component.html',
  styleUrls: ['./receipt.component.css']
})
export class ReceiptComponent implements OnInit {

  expenses: Expense[];
  expense: Expense = new Expense();

  //accounts: string[];
  //account: string;

  //colorCodes: string[];
  //colorCode: string;

  //categories: string[];
  //category: string;

  constructor(
    //private sharedDataService: SharedDataService
) { }

  ngOnInit() {
    //this.accounts = this.sharedDataService.getAccounts();
    //this.colorCodes = this.sharedDataService.getColorCodes();
    //this.categories = this.sharedDataService.getCategories();
  }

}
