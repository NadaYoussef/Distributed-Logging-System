import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogListComponent } from './log-list/log-list.component';
import { LogDetailsComponent } from './log-details/log-details.component';

const routes: Routes = [
  { path: '', component: LogListComponent },
  { path: 'log/:id', component: LogDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
