namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using System.Collections.Generic;
using RaspiRobot.RobotControl.Settings;

public interface IGrabItDriver
{
    void Initialize();

    void Execute(IReadOnlyList<Sequence> sequences);
}