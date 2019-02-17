import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api'

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {

  constructor() { }

  getAccounts() {

    let accounts = ['--Select An Account--', 'Suntrust', 'Middleburg', 'Wells Fargo'];
    return accounts;
  }

  getColorCodes() {
    let colorCodes = ['--Select a Color', 'Grey', 'Black', 'Red', 'Orange', 'yellow', 'Green', 'Blue', 'Purple', 'Pink', 'Cornflower-Blue'];
    return colorCodes;
  }

  getCategories() {
    let categories = ['--Select a Category--', 'Rebalance', 'Tax Return', 'Offering', 'Cell', 'Utilities',
      'Mortgage', 'Groceries', 'Car Gas', 'Trips', 'Birthdays', 'Celebrations',
      'Christmas', 'Kittens', 'Preschool', 'Car Expenses', 'EZ Pass', 'Subscriptions',
      'Stuff I Forgot to Budget For', 'Auto Loan', 'Student Loan', 'Jujitsu/Krav Maga',
      'Swimming', 'VA529', 'Training Fund', 'Sports Gym', 'Dining Out', 'Fun Money']
    return categories;
  }

    
  
}
