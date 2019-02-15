import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { ExpenseQueryService } from './ExpenseQuery.Service';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ExpenseDeletionService } from './expenseDeletion.service';
import { ExpenseCreationService } from './expenseCreation.service';
import { ExpenseDocumentorComponent } from './expense-documentor/expense-documentor.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { ExpenseModificationService } from './expenseModification.service';

@NgModule({
  declarations: [
    AppComponent,
    ExpenseDocumentorComponent,
    HomeComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    //Observable
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule
   
  ],
  providers: [ExpenseQueryService,
    ExpenseDeletionService,
    ExpenseCreationService,
    ExpenseModificationService
],
  bootstrap: [AppComponent],
})
export class AppModule { }
