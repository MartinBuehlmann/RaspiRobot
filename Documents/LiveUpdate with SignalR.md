# LiveUpdate with SignalR

Some features require to update values on the Angualar frontend without having
to refresh the UI manually. This can be done by using WebSockets or as it is
realized in this software, by using SignalR (which is based on WebSockets on
modern browsers).

## How-To

### Define the SignalR Hub

Define te SignalR Hub in the Web project of the backend:

> public class OperationModeChangedHub : Hub;

Register the hub in the Startup.cs as endpoint:

> app.UseEndpoints(endpoints =>
> {
>   endpoints.MapHub<OperationModeChangedHub>($"/{nameof(OperationModeChangedHub)}");
>   endpoints.MapControllers();
> });

### Send a message to the frontend

Even if there may be other sources of events to be raised to the UI, often
the starting point is a EventBroker event, handled by an observer class:

> internal class OperationModeChangedObserver :
>     IEventSubscriptionAsync<OperationModeChangedEvent>,
>     ILiveUpdateEventObserver
> {
>     private readonly IOperationModeRetriever operationModeRetriever;
>     private readonly IHubContext<OperationModeChangedHub> operationModeChangedHub;
> 
>     public OperationModeChangedObserver(
>         IOperationModeRetriever operationModeRetriever,
>         IHubContext<OperationModeChangedHub> operationModeChangedHub)
>     {
>         this.operationModeRetriever = operationModeRetriever;
>         this.operationModeChangedHub = operationModeChangedHub;
>     }
> 
>     public async Task HandleAsync(OperationModeChangedEvent data)
>     {
>         await this.operationModeChangedHub.Clients.All
>             .SendAsync(
>                 "UpdateOperationMode",
>                 this.operationModeRetriever.OperationMode.ToString());
>     }
> }

The interface ILiveUpdateEventObserver is used by a IBackgroundService based
class that ensures that after startup all observers get initialized.

### Receiving a message on the frontend

Implement a service to deal with the live update in Angular:

> import { Injectable } from '@angular/core';
> import * as signalR from '@microsoft/signalr';
> 
> @Injectable({
>   providedIn: 'root'
> })
> export class OperationModeChangedService {
>   private hubConnection: signalR.HubConnection;
> 
>   constructor() {
>       this.hubConnection = new signalR.HubConnectionBuilder()
>           .withUrl("/OperationModeChangedHub")
>            .build();
> 
>       this.hubConnection.start();
>   }
> 
>   updateOperationMode(callback: (operationMode: any) => void) {
>       this.hubConnection.on("UpdateOperationMode", (operationMode: any) => {
>           callback(operationMode);
>       })
>   }
> }

Use this service in the class where you are interested for the specific event:

> ngOnInit(): void {
>     this.operationModeChangedService.updateOperationMode(operationMode => this.operationMode = operationMode);
> }
