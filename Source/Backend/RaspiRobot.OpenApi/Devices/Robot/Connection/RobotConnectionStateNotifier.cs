namespace RaspiRobot.OpenApi.Devices.Robot.Connection;

using System.Threading.Tasks;
using Erowa.OpenAPI.Robot.Connection;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RaspiRobot.RobotControl.Devices.Connection;

internal class RobotConnectionStateNotifier : IConnectionStateNotifier
{
    private readonly IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream;

    public RobotConnectionStateNotifier(
        IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream)
    {
        this.responseStream = responseStream;
    }

    public async Task NotifyAsync(bool isConnected)
    {
        await this.responseStream.WriteAsync(
            isConnected
                ? new RetrieveConnectionStateChangedResponse { Connected = new Empty() }
                : new RetrieveConnectionStateChangedResponse { Disconnected = new Empty() });
    }
}