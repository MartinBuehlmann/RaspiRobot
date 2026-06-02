namespace RaspiRobot.OpenApi.Devices.Robot.Busy;

using System.Threading.Tasks;
using Erowa.OpenAPI.Robot.Busy;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

internal class RobotBusyService : Erowa.OpenAPI.Robot.Busy.RobotBusyService.RobotBusyServiceBase
{
    // TODO: Implement
    public override Task RetrieveBusyStateChanged(
        Empty request,
        IServerStreamWriter<RetrieveBusyStateChangedResponse> responseStream,
        ServerCallContext context)
    {
        return base.RetrieveBusyStateChanged(request, responseStream, context);
    }
}