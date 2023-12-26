import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class OperationModeChangedService {
  private hubConnection: signalR.HubConnection;

  constructor() { 
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/OperationModeChangedHub")
      .build();

    this.hubConnection.start();
  }

  updateOperationMode(callback: (operationMode: any) => void) {
    this.hubConnection.on("UpdateOperationMode", (operationMode: any) => {
      callback(operationMode);
    })
  }
}
