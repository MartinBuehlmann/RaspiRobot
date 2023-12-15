namespace RaspiRobot.RobotControl.Settings;

using System.Collections.Generic;
using Newtonsoft.Json;

public interface IJsonConverterProvider
{
    IReadOnlyList<JsonConverter> JsonConverters { get; }
}