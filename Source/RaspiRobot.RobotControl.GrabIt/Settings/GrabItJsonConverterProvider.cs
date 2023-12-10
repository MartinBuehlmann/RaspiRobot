namespace RaspiRobot.RobotControl.GrabIt.Settings;

using System.Collections.Generic;
using Newtonsoft.Json;
using RaspiRobot.RobotControl.Devices;

internal class GrabItJsonConverterProvider : IJsonConverterProvider
{
    public IReadOnlyList<JsonConverter> JsonConverters { get; }
        = new[]
        {
            new GrabItPositionConverter(),
        };
}