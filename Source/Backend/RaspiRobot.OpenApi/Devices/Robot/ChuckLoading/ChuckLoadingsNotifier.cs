namespace RaspiRobot.OpenApi.Devices.Robot.ChuckLoading;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erowa.OpenAPI.Robot;
using EventBroker;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Robot.ChuckLoading;
using ChuckLoading = Erowa.OpenAPI.Robot.ChuckLoading;

internal class ChuckLoadingsNotifier : IChuckLoadingsNotifier, IEventSubscriptionAsync<ChuckLoadingChangedEvent>
{
    private readonly ChuckLoadingConverter chuckLoadingConverter;
    private readonly IServerStreamWriter<ChuckLoadingsResponse> responseStream;

    public ChuckLoadingsNotifier(
        ChuckLoadingConverter chuckLoadingConverter,
        IServerStreamWriter<ChuckLoadingsResponse> responseStream)
    {
        this.chuckLoadingConverter = chuckLoadingConverter;
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(IReadOnlyList<RobotControl.Devices.Robot.ChuckLoading.ChuckLoading> chuckLoadings)
    {
        IReadOnlyList<ChuckLoading> robotChuckLoadings = chuckLoadings.Select(x => this.chuckLoadingConverter.Convert(x)).ToList();
        await this.responseStream.WriteAsync(
            new ChuckLoadingsResponse
            {
                Loadings = { robotChuckLoadings },
            });
    }

    public async Task HandleAsync(ChuckLoadingChangedEvent data)
    {
        await this.NotifyAsync(data.ChuckLoadings);
    }
}