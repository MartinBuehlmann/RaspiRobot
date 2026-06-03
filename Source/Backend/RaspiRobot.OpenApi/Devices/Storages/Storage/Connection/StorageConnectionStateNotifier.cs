namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Connection;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage.Connection;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Connection;

internal class StorageConnectionStateNotifier : IConnectionStateNotifier
{
    private readonly IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream;

    public StorageConnectionStateNotifier(
        IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream)
    {
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(bool isConnected)
    {
        await this.responseStream.WriteAsync(
            isConnected
                ? new RetrieveConnectionStateChangedResponse { Connected = new Empty() }
                : new RetrieveConnectionStateChangedResponse { Disconnected = new Empty() });
    }
}