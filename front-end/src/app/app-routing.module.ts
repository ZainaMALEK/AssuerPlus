import { AuthenticationPageComponent } from './authentication/authentication-page/authentication-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CustomAreaComponent} from './customer-area/custom-area/custom-area.component';
const routes: Routes = [
  { path: 'authentication', component: AuthenticationPageComponent },
  { path: 'customArea', component: CustomAreaComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
