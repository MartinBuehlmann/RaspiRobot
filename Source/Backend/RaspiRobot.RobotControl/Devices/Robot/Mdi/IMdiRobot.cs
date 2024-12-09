namespace RaspiRobot.RobotControl.Devices.Robot.Mdi;

using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Robot.Steps;

public interface IMdiRobot
{
    Task<Position?> StepAsync(Axis axis, AxisDirection direction, int stepSize);
}