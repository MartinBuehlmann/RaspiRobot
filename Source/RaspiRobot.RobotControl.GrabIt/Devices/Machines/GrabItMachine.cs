namespace RaspiRobot.RobotControl.GrabIt.Devices.Machines;

using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Settings;

internal class GrabItMachine : IMachine
{
    private readonly MachineSettings settings;

    public GrabItMachine(MachineSettings settings)
    {
        this.settings = settings;
    }
}