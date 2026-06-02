namespace RaspiRobot.OpenApi.Devices.Robot.ChuckOccupancy;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erowa.OpenAPI.MachineLoading;
using EventBroker;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot.ChuckOccupancy;
using ChuckOccupancy = Erowa.OpenAPI.MachineLoading.ChuckOccupancy;

internal class ChuckOccupancyNotifier : IChuckOccupancyNotifier, IEventSubscriptionAsync<ChuckOccupancyChangedEvent>
{
    private readonly ChuckOccupancyConverter chuckOccupancyConverter;
    private readonly IServerStreamWriter<RetrieveMachineLoadingChangedResponse> responseStream;

    public ChuckOccupancyNotifier(
        ChuckOccupancyConverter chuckOccupancyConverter,
        IServerStreamWriter<RetrieveMachineLoadingChangedResponse> responseStream)
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
            new RetrieveMachineLoadingChangedResponse
            {
                Ready = new Ready { Chucks = { robotChuckOccupancies } },
            });
    }

    public async Task HandleAsync(ChuckOccupancyChangedEvent data)
    {
        await this.NotifyAsync(data.ChuckOccupancies);
    }
}