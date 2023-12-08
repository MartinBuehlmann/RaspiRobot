namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using RaspiRobot.RobotControl.GrabIt.Settings;
using RaspiRobot.RobotControl.Settings;

public interface IGrabItDriver
{
    IReadOnlyDictionary<byte, int> CurrentDrivePositions { get; }

    void Initialize();

    void Execute(IReadOnlyList<GrabItPosition> positions);
}