namespace RaspiRobot.OpenApi.Devices.Robot.Alarms;

using System.Threading;
using System.Threading.Tasks;
using Common.DependencyInjection;
using Erowa.OpenAPI.Robot.Alarm;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using RaspiRobot.RobotControl;
using RaspiRobot.RobotControl.Devices.Robot;

internal class RobotAlarmService : Erowa.OpenAPI.Robot.Alarm.RobotAlarmService.RobotAlarmServiceBase
{
    private readonly IDeviceService deviceService;
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly Factory factory;

    public RobotAlarmService(
        IDeviceService deviceService,
        IHostApplicationLifetime hostApplicationLifetime,
        Factory factory)
    {
        this.deviceService = deviceService;
        this.hostApplicationLifetime = hostApplicationLifetime;
        this.factory = factory;
    }

    public override async Task RetrieveAlarmsChanged(
        Empty request,
        IServerStreamWriter<RetrieveAlarmsChangedResponse> responseStream,
        ServerCallContext context)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            context.CancellationToken,
            this.hostApplicationLifetime.ApplicationStopping);

        var robotAlarmsNotifier = this.factory.Create<RobotAlarmsNotifier>(responseStream);
        IRobot robot = this.deviceService.RetrieveRobot();
        await robot.SubscribeForAlarmsChangedAsync(robotAlarmsNotifier, cancellationTokenSource.Token);
    }
}