namespace RaspiRobot.OpenApi.Devices.Robot.Connection;

using System.Threading.Tasks;
using Erowa.OpenAPI.Robot.Connection;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

internal class RobotConnectionService : Erowa.OpenAPI.Robot.Connection.RobotConnectionService.RobotConnectionServiceBase
{
    // TODO: Implement
    public override Task RetrieveConnectionStateChanged(
        Empty request,
        IServerStreamWriter<RetrieveConnectionStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        return base.RetrieveConnectionStateChanged(request, responseStream, context);
    }
}