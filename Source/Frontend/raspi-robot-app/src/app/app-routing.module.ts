import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { MdiComponent } from './mdi/mdi.component';
import { RobotComponent } from './devices/robot/robot.component';
import { StorageComponent } from './devices/storage/storage.component';

const routes: Routes = [
  { path:'', component: HomeComponent },
  { path:'home', component: HomeComponent },
  { path:'storage', component: StorageComponent },
  { path:'robot', component: RobotComponent },
  { path:'mdi', component: MdiComponent },
  { path:'about', component: AboutComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
