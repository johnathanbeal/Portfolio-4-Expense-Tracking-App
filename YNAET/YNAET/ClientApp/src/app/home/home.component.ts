import { Component, OnInit } from '@angular/core';
//import { SharedDataService } from '../sharedData.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

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
