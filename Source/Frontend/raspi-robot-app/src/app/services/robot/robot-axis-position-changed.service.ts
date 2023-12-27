import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class RobotAxisPositionChangedService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/RobotAxisPositionChangedHub")
      .build();

    this.hubConnection.start();
   }

   robotAxisPositionChanged(callback: () => void) {
    this.hubConnection.on("RobotAxisPositionChanged", () => {
      callback();
    })
  }
}
