import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '../../node_modules/@angular/router';
import { ExpenseDocumentorComponent } from './expense-documentor/expense-documentor.component';
import { ReceiptComponent } from './receipt/receipt.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'receipts', component: ReceiptComponent},
  { path: 'expenses', component: ExpenseDocumentorComponent },

]

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule]
})
export class AppRoutingModule { }
