import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '../../node_modules/@angular/router';
import { ExpenseDocumentorComponent } from './expense-documentor/expense-documentor.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'expenses', component: ExpenseDocumentorComponent }

]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule]
})
export class AppRoutingModule { }
