namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;
using System.Threading.Tasks;
using RaspiRobot.RobotControl.Devices.Machines;
using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages;
using RaspiRobot.RobotControl.Devices.Storages.AutoLinkMagazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Settings;

public interface ISettingsRetriever
{
    Task<RobotSettings> RetrieveRobotSettingsAsync();

    Task<IReadOnlyList<AutoLinkMagazineSettings>> RetrieveAutoLinkMagazineSettingsAsync();

    Task<IReadOnlyList<MagazineSettings>> RetrieveMagazineSettingsAsync();

    Task<IReadOnlyList<LoadingStationSettings>> RetrieveLoadingStationSettingsAsync();

    Task<ChuckSettings> RetrieveByAsync(MachineChuck chuck);

    Task<PlaceSettings> RetrieveByAsync(StoragePlace place);
}