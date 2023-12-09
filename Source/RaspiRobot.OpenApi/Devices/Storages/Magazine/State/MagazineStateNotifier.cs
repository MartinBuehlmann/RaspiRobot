namespace RaspiRobot.OpenApi.Devices.Storages.Magazine.State;

using System.Threading.Tasks;
using Erowa.OpenAPI.Storage;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.Magazine;

public class MagazineStateNotifier : IMagazineStateNotifier
{
    private readonly MagazineStateConverter magazineStateConverter;
    private readonly IServerStreamWriter<MagazineStateResponse> responseStream;

    public MagazineStateNotifier(
        MagazineStateConverter magazineStateConverter,
        IServerStreamWriter<MagazineStateResponse> responseStream)
    {
        this.magazineStateConverter = magazineStateConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(State state)
    {
        var magazineState = this.magazineStateConverter.Convert(state);
        await this.responseStream.WriteAsync(
            new MagazineStateResponse
            {
                State = magazineState,
            });
    }
}