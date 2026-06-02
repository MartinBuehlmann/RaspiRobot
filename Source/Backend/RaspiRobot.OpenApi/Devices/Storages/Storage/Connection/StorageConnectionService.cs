namespace RaspiRobot.OpenApi.Devices.Storages.Storage.Connection;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage.Connection;
using Grpc.Core;

internal class StorageConnectionService : Erowa.OpenAPI.Storage.Connection.StorageConnectionService.StorageConnectionServiceBase
{
    // TODO: Implement
    public override Task RetrieveConnectionStateChanged(
        RetrieveConnectionStateChangedRequest request,
        IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        return base.RetrieveConnectionStateChanged(request, responseStream, context);
    }
}