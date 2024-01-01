import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DeviceModel } from '../../services/devices/device-model';
import { StorageModel } from './storage-model';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrl: './storage.component.scss'
})
export class StorageComponent implements OnInit {
  storage: StorageModel | undefined;
  
  constructor(
    private route : ActivatedRoute) {}
  
  ngOnInit(): void {
    this.route.params.subscribe(params =>
        this.storage = new StorageModel(params["name"], params["deviceType"]))
  }
}
