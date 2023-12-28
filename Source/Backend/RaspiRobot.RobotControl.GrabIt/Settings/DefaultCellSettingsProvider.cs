namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Devices.Machines.Settings;
using RaspiRobot.RobotControl.Devices.Robot.Settings;
using RaspiRobot.RobotControl.Devices.Storages.LoadingStation.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Magazine.Settings;
using RaspiRobot.RobotControl.Devices.Storages.Settings;
using RaspiRobot.RobotControl.Settings;

internal class DefaultCellSettingsProvider : IDefaultCellSettingsProvider
{
    public CellSettings DefaultCellSettings { get; } =
        new CellSettings(
            new RobotSettings(
                "GrabIt",
                new Sequence
                {
                    Steps =
                    {
                        new Step
                        {
                            Positions =
                            {
                                new Position(2, 300),
                                new Position(4, 300),
                                new Position(5, 320),
                                new Position(0, 300),
                                new Position(1, 300),
                                new Position(3, 200),
                            },
                        },
                    },
                },
                new Sequence
                {
                    Steps =
                    {
                        new Step
                        {
                            Positions =
                            {
                                new Position(1, 300),
                                new Position(2, 300),
                                new Position(3, 200),
                                new Position(4, 300),
                            },
                        },
                    },
                }))
        {
            Machines =
            {
                new MachineSettings("M1")
                {
                    Chucks =
                    {
                        new ChuckSettings(
                            1,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 510),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 510),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            }),
                    },
                },
            },
            LoadingStations =
            {
                new LoadingStationSettings(
                    1,
                    "LS",
                    new[]
                    {
                        new PlaceSettings(
                            1,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 95),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 95),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            }),
                    }),
            },
            Magazines =
            {
                new MagazineSettings(
                    2,
                    "Mag",
                    new[]
                    {
                        new PlaceSettings(
                            2,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 160),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 160),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            3,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 250),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 250),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            4,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 350),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 350),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            5,
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 440),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            },
                            new Sequence
                            {
                                Steps =
                                {
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(0, 440),
                                            new Position(1, 300),
                                            new Position(2, 300),
                                            new Position(3, 200),
                                            new Position(4, 300),
                                            new Position(5, 280),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(1, 250),
                                            new Position(2, 230),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(5, 320),
                                        },
                                    },
                                    new Step
                                    {
                                        Positions =
                                        {
                                            new Position(2, 300),
                                            new Position(1, 300),
                                        },
                                    },
                                },
                            }),
                    }),
            },
        };
}