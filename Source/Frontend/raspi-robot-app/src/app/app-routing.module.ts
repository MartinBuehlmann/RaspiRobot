import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { DevicesComponent } from './devices/devices.component';
import { MdiComponent } from './mdi/mdi.component';

const routes: Routes = [
  { path:'', component: HomeComponent },
  { path:'devices', component: DevicesComponent },
  { path:'mdi', component: MdiComponent },
  { path:'about', component: AboutComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
