import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { DevicesComponent } from './devices/devices.component';
import { MdiComponent } from './mdi/mdi.component';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FooterComponent } from './footer/footer.component';
import { OperationModeComponent } from './operation-mode/operation-mode.component';
import { OperationModeService } from './services/operation-mode/operation-mode.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RobotComponent } from './devices/robot/robot.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DevicesComponent,
    MdiComponent,
    AboutComponent,
    HomeComponent,
    NavigationComponent,
    FooterComponent,
    OperationModeComponent,
    RobotComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [OperationModeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
