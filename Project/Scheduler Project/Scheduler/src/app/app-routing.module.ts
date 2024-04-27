import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentLandingComponent } from './appointment-landing/appointment-landing.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { AddComponent } from './add/add.component';
import { DetailComponent } from './detail/detail.component';
import { AppointmentComponent } from './appointment/appointment.component';

const routes: Routes = [
  
  {path: "home", component: HomeComponent, children : [
    {path: "appointment", component: AppointmentLandingComponent, children: [
      {path: "all", component: AppointmentComponent},
      {path: "detail/:id", component: DetailComponent},
      {path: "", component: AppointmentComponent},
    ]},
    {path: "", component: AppointmentLandingComponent},
    {path: "profile", component: ProfileComponent},
    {path: "add", component: AddComponent},
  ]},
  {path: "", redirectTo: "home", pathMatch: "full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
