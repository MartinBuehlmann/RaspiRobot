import { Component, OnInit } from '@angular/core';
import { DevicesService } from '../services/devices/devices.service';
import { DevicesModel } from '../services/devices/devices-model';
import { DeviceModel } from '../services/devices/device-model';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navigation',
  imports: [NgFor, RouterLink],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss'
})
export class NavigationComponent implements OnInit {
  robot: DeviceModel | undefined;
  storages: DeviceModel[] | undefined;
  
  constructor(
    private devicesService: DevicesService) { }
  
  ngOnInit(): void {
    this.devicesService.getDevices()
    .subscribe(
      (allDevices: DevicesModel) => {
        this.robot = allDevices.robot;
        this.storages = allDevices.storages;
      },
      (error) => console.error('Error fetching all devices: ', error)
    );
  }
}
