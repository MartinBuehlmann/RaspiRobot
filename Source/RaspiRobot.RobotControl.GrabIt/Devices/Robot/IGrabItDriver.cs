namespace RaspiRobot.RobotControl.GrabIt.Devices.Robot;

using RaspiRobot.RobotControl.Settings;

public interface IGrabItDriver
{
    void Initialize();

    void ExecuteSequence(Sequence sequence);
}