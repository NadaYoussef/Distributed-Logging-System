import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component'; 
import { LogListComponent } from './log-list/log-list.component'; 
import { LogDetailsComponent } from './log-details/log-details.component'; 
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module'; 
import { MatTableModule } from '@angular/material/table';
import { FormsModule } from '@angular/forms'; /
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  declarations: [
    AppComponent,
    LogListComponent,
    LogDetailsComponent 
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule, 
    MatTableModule, 
    FormsModule, 
    MatPaginatorModule, 
  ],
  providers: [],
  bootstrap: [AppComponent] 
})
export class AppModule { }
