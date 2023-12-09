namespace RaspiRobot.RobotControl.Devices;

using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.Settings;

public interface ISettingsRetriever
{
    Task<RobotSettings> RetrieveRobotSettingsAsync();

    Task<ChuckSettings> RetrieveByAsync(MachineChuck chuck);

    Task<PlaceSettings> RetrieveByAsync(StoragePlace place);
}