namespace RaspiRobot.OpenApi.Devices.Storages.AutoLinkMagazine.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine;

public class AutoLinkMagazineStateNotifier : IAutoLinkMagazineStateNotifier
{
    private readonly AutoLinkMagazineStateConverter autoLinkMagazineStateConverter;
    private readonly IServerStreamWriter<AutoLinkMagazineStateResponse> responseStream;

    public AutoLinkMagazineStateNotifier(
        AutoLinkMagazineStateConverter autoLinkMagazineStateConverter,
        IServerStreamWriter<AutoLinkMagazineStateResponse> responseStream)
    {
        this.autoLinkMagazineStateConverter = autoLinkMagazineStateConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(State state)
    {
        AutoLinkMagazineState magazineState = this.autoLinkMagazineStateConverter.Convert(state);
        await this.responseStream.WriteAsync(
            new AutoLinkMagazineStateResponse
            {
                State = magazineState,
            });
    }
}