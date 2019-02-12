import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { RouterModule, Routes } from '@angular/router';
//import { Observable, of, from } from 'rxjs';
import { ExpenseQueryService } from './ExpenseQuery.Service';
import { Router } from '@angular/router';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ExpenseDeletionService } from './expenseDeletion.service';
import { ExpenseCreationService } from './expenseCreation.service';
import { ExpenseDocumentorComponent } from './expense-documentor/expense-documentor.component';

//const appRoutes: Routes = [
//  { path: 'app-expense-documentor', component: ExpenseDocumentorComponent, data: { title: 'Document An Expense' } }
//];


@NgModule({
  declarations: [
    AppComponent,
    ExpenseDocumentorComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    //Observable
    HttpClientModule,
    ReactiveFormsModule,
    //RouterModule.forRoot(
    //  appRoutes,
    //  { enableTracing: true }
    
  ],
  providers: [ExpenseQueryService,
    ExpenseDeletionService,
    ExpenseCreationService
],
  bootstrap: [AppComponent],
})
export class AppModule { }
