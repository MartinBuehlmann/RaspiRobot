import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DevicesModel } from './devices-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DevicesService {
  constructor(private http : HttpClient) { }

  getAllDevices() : Observable<DevicesModel> {
    return this.http.get<DevicesModel>(`web/Devices/All`);
  }
}