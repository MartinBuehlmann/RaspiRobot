namespace RaspiRobot.OpenApi.Devices.Storages.LoadingStation.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation;

public class LoadingStationStateNotifier : ILoadingStationStateNotifier
{
    private readonly LoadingStationStateConverter loadingStationStateConverter;
    private readonly IServerStreamWriter<LoadingStationStateResponse> responseStream;

    public LoadingStationStateNotifier(
        LoadingStationStateConverter loadingStationStateConverter,
        IServerStreamWriter<LoadingStationStateResponse> responseStream)
    {
        this.loadingStationStateConverter = loadingStationStateConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(State state)
    {
        LoadingStationState loadingStationState = this.loadingStationStateConverter.Convert(state);
        await this.responseStream.WriteAsync(
            new LoadingStationStateResponse
            {
                State = loadingStationState,
            });
    }
}