namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erowa.OpenAPI.Robot;
using EventBroker;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using ChuckOccupancy = Erowa.OpenAPI.Robot.ChuckOccupancy;

internal class ChuckOccupancyNotifier : IChuckOccupancyNotifier, IEventSubscriptionAsync<ChuckOccupancyChangedEvent>
{
    private readonly ChuckOccupancyConverter chuckOccupancyConverter;
    private readonly IServerStreamWriter<ChuckOccupancyResponse> responseStream;

    public ChuckOccupancyNotifier(
        ChuckOccupancyConverter chuckOccupancyConverter,
        IServerStreamWriter<ChuckOccupancyResponse> responseStream)
    {
        this.chuckOccupancyConverter = chuckOccupancyConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(
        IReadOnlyList<RobotControl.Devices.Robot.ChuckOccupancy.ChuckOccupancy> chuckOccupancies)
    {
        IReadOnlyList<ChuckOccupancy> robotChuckOccupancies =
            chuckOccupancies.Select(x => this.chuckOccupancyConverter.Convert(x)).ToList();
        await this.responseStream.WriteAsync(
            new ChuckOccupancyResponse
            {
                Occupancies = { robotChuckOccupancies },
            });
    }

    public async Task HandleAsync(ChuckOccupancyChangedEvent data)
    {
        await this.NotifyAsync(data.ChuckOccupancies);
    }
}