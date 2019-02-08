import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
//import { Observable, of, from } from 'rxjs';
import { ExpenseQueryService } from './ExpenseQuery.Service';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';

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
  providers: [ExpenseQueryService],
  bootstrap: [AppComponent]
})
export class AppModule { }
