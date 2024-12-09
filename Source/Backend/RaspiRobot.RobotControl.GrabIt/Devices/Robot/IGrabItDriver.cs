namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Robot.Steps;

internal interface IGrabItDriver
{
    IReadOnlyDictionary<byte, int> CurrentDrivePositions { get; }

    void Initialize();

    Task ExecuteAsync(IReadOnlyList<Position> positions);
}