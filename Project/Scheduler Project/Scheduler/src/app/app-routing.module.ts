import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentLandingComponent } from './appointment-landing/appointment-landing.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { AddComponent } from './add/add.component';

const routes: Routes = [
  
  {path: "home", component: HomeComponent, children : [
    {path: "", component: AppointmentLandingComponent},
    {path: "appointment", component: AppointmentLandingComponent},
    {path: "profile", component: ProfileComponent},
    {path: "add", component: AddComponent}
  ]},
  {path: "", redirectTo: "home", pathMatch: "full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: "reload"})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
