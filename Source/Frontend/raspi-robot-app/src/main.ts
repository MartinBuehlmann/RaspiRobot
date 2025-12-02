import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';


import { OperationModeService } from './app/services/operation-mode/operation-mode.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { AppRoutingModule } from './app/app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app/app.component';
import { importProvidersFrom, provideZoneChangeDetection } from '@angular/core';


bootstrapApplication(AppComponent, {
    providers: [
        provideZoneChangeDetection(),importProvidersFrom(BrowserModule, AppRoutingModule, FormsModule),
        OperationModeService, provideHttpClient(withInterceptorsFromDi())
    ]
})
  .catch(err => console.error(err));
