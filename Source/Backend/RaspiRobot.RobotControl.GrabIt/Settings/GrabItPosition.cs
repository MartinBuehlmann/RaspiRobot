﻿namespace RaspiRobot.RobotControl.GrabIt.Settings;

using RaspiRobot.RobotControl.Settings;

internal class GrabItPosition(byte drive, int value) : IPosition
{
    public byte Drive { get; } = drive;

    public int Value { get; } = value;
}