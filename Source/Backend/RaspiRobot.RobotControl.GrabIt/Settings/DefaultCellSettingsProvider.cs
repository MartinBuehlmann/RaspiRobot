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
                new SequenceSettings
                {
                    Steps =
                    {
                        new MoveStepSettings
                        {
                            Positions =
                            {
                                new PositionSettings(2, 300),
                                new PositionSettings(4, 300),
                                new PositionSettings(5, 320),
                                new PositionSettings(0, 300),
                                new PositionSettings(1, 300),
                                new PositionSettings(3, 200),
                            },
                        },
                    },
                },
                new SequenceSettings
                {
                    Steps =
                    {
                        new MoveStepSettings
                        {
                            Positions =
                            {
                                new PositionSettings(1, 300),
                                new PositionSettings(2, 300),
                                new PositionSettings(3, 200),
                                new PositionSettings(4, 300),
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
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 510),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new ChuckLoadingChangedNotificationStepSettings(),
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            },
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 510),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new ChuckLoadingChangedNotificationStepSettings(),
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
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
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 95),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            },
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 95),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
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
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 160),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            },
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 160),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            3,
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 250),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            },
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 250),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            4,
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 350),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            },
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 350),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            }),
                        new PlaceSettings(
                            5,
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 440),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            },
                            new SequenceSettings
                            {
                                Steps =
                                {
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(0, 440),
                                            new PositionSettings(1, 300),
                                            new PositionSettings(2, 300),
                                            new PositionSettings(3, 200),
                                            new PositionSettings(4, 300),
                                            new PositionSettings(5, 280),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(1, 250),
                                            new PositionSettings(2, 230),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(5, 320),
                                        },
                                    },
                                    new MoveStepSettings
                                    {
                                        Positions =
                                        {
                                            new PositionSettings(2, 300),
                                            new PositionSettings(1, 300),
                                        },
                                    },
                                },
                            }),
                    }),
            },
        };
}