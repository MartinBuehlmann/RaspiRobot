namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage.VisuLink;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Storages.State;

internal class StorageStateNotifier : IStorageStateNotifier
{
    private readonly StorageStateConverter storageStateConverter;
    private readonly IServerStreamWriter<RetrieveStateChangedResponse> responseStream;

    public StorageStateNotifier(
        StorageStateConverter storageStateConverter,
        IServerStreamWriter<RetrieveStateChangedResponse> responseStream)
    {
        this.storageStateConverter = storageStateConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(State state)
    {
        await this.responseStream.WriteAsync(
            this.storageStateConverter.Convert(state));
    }
}