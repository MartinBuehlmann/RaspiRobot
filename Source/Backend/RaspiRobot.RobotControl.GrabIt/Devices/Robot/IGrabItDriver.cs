namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Settings;

internal interface IGrabItDriver
{
    IReadOnlyDictionary<byte, int> CurrentDrivePositions { get; }

    void Initialize();

    void Execute(IReadOnlyList<Position> positions);
}