namespace RaspiRobot.RobotControl.Devices;

using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Magazine;
using RaspiRobot.RobotControl.Settings;

public interface ISettingsRetriever
{
    Task<RobotSettings> RetrieveRobotSettingsAsync();

    Task<ChuckSettings> RetrieveByAsync(MachineChuck chuck);

    Task<PlaceSettings> RetrieveByAsync(StoragePlace place);
}