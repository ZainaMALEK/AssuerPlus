import { AuthenticationPageComponent } from './authentication/authentication-page/authentication-page.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CustomAreaComponent} from './customer-area/custom-area/custom-area.component';
import { DeclareAccidentComponent } from './customer-area/declare-accident/declare-accident.component';
import{ AuthGuardService} from './guards/auth-guard.service';
import { HomeComponent } from './customer-area/home/home.component';
const routes: Routes = [
  { path: '', component: AuthenticationPageComponent },
  { path: 'authentication', component: AuthenticationPageComponent },
  { path: 'customArea', component: CustomAreaComponent, canActivate: [AuthGuardService],
  children: [
    { path: 'accident', component: DeclareAccidentComponent },
    { path: 'home', component: HomeComponent },

  ] },
  { path: 'accident', component: DeclareAccidentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
