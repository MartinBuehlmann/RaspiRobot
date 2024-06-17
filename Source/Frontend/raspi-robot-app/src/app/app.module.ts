import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { MdiComponent } from './mdi/mdi.component';
import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FooterComponent } from './footer/footer.component';
import { OperationModeComponent } from './operation-mode/operation-mode.component';
import { OperationModeService } from './services/operation-mode/operation-mode.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RobotComponent } from './devices/robot/robot.component';
import { StorageComponent } from './devices/storage/storage.component';

@NgModule({ declarations: [
        AppComponent,
        HeaderComponent,
        MdiComponent,
        AboutComponent,
        HomeComponent,
        NavigationComponent,
        FooterComponent,
        OperationModeComponent,
        RobotComponent,
        StorageComponent,
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        AppRoutingModule,
        FormsModule], providers: [OperationModeService, provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
