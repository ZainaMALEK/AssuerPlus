import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationPageComponent } from './authentication/authentication-page/authentication-page.component';
import { NavBarComponent } from './customer-area/nav-bar/nav-bar.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { CustomAreaComponent } from './customer-area/custom-area/custom-area.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { DeclareAccidentComponent } from './customer-area/declare-accident/declare-accident.component';
import { FormsModule }   from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { HomeComponent } from './customer-area/home/home.component';
import {MatDialogModule} from '@angular/material/dialog';
import { DialogSinistreDetailComponent } from './customer-area/dialog-sinistre-detail/dialog-sinistre-detail.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}
@NgModule({
  declarations: [
    AppComponent,
    AuthenticationPageComponent,
    NavBarComponent,
    CustomAreaComponent,
    DeclareAccidentComponent,
    HomeComponent,
    DialogSinistreDetailComponent,

  ],
  imports: [
    MatDialogModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        disallowedRoutes: []
      }
    })

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
