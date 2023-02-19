import { AuthenticationPageComponent } from './authentication/authentication-page/authentication-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CustomAreaComponent} from './customer-area/custom-area/custom-area.component';
import { DeclareAccidentComponent } from './customer-area/declare-accident/declare-accident.component';
const routes: Routes = [
  { path: 'authentication', component: AuthenticationPageComponent },
  { path: 'customArea', component: CustomAreaComponent,
  children: [
    { path: 'accident', component: DeclareAccidentComponent },

  ] },
  { path: 'accident', component: DeclareAccidentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
