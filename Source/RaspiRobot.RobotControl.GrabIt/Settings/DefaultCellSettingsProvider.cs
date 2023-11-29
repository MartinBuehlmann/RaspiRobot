namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Devices;
using RaspiRobot.RobotControl.Settings;

public class DefaultCellSettingsProvider : IDefaultCellSettingsProvider
{
    public CellSettings DefaultCellSettings { get; } =
        new CellSettings(new RobotSettings("GrabIt", new Position()))
        {
            Machines = { new MachineSettings("M1", new Position()) },
            Magazines =
            {
                new MagazineSettings(1, "P1", new Position())
                {
                    Places =
                    {
                        new PlaceSettings(1, new Position()),
                        new PlaceSettings(2, new Position()),
                        new PlaceSettings(3, new Position()),
                        new PlaceSettings(4, new Position()),
                    },
                },
            },
        };
}