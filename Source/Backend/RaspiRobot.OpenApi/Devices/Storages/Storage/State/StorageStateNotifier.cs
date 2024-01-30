namespace RaspiRobot.OpenApi.Devices.Storages.Storage.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Storages;

internal class StorageStateNotifier : IStorageStateNotifier
{
    private readonly StorageStateConverter storageStateConverter;
    private readonly IServerStreamWriter<StorageStateResponse> responseStream;

    public StorageStateNotifier(
        StorageStateConverter storageStateConverter,
        IServerStreamWriter<StorageStateResponse> responseStream)
    {
        this.storageStateConverter = storageStateConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(State state)
    {
        var storageState = this.storageStateConverter.Convert(state);
        await this.responseStream.WriteAsync(
            new StorageStateResponse
            {
                State = storageState,
            });
    }
}