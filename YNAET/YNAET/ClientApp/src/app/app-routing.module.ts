import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '../../node_modules/@angular/router';
import { ExpenseDocumentorComponent } from './expense-documentor/expense-documentor.component';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'home', component: HomeComponent },
  { path: 'expenses', component: ExpenseDocumentorComponent },

]

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule]
})
export class AppRoutingModule { }
