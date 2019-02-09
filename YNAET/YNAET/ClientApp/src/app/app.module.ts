import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
//import { Observable, of, from } from 'rxjs';
import { ExpenseQueryService } from './ExpenseQuery.Service';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ExpenseDeletionService } from './expenseDeletion.service';

@NgModule({
  declarations: [
    AppComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    //Observable
    HttpClientModule
    
  ],
  providers: [ExpenseQueryService, ExpenseDeletionService],
  bootstrap: [AppComponent]
})
export class AppModule { }
