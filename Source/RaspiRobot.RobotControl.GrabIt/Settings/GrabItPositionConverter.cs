namespace RaspiRobot.RobotControl.GrabIt.Settings;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RaspiRobot.RobotControl.Settings;

public class GrabItPositionConverter : JsonConverter<IPosition>
{
    public override void WriteJson(
        JsonWriter writer,
        IPosition? value,
        JsonSerializer serializer)
    {
        var grabItPosition = (GrabItPosition)value!;
        writer.WriteStartObject();
        writer.WritePropertyName("Drive");
        writer.WriteValue(grabItPosition.Drive);
        writer.WritePropertyName("Value");
        writer.WriteValue(grabItPosition.Value);
        writer.WriteEndObject();
    }

    public override IPosition? ReadJson(
        JsonReader reader,
        Type objectType,
        IPosition? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        int drive = jsonObject["Drive"]!.Value<int>();
        int value = jsonObject["Value"]!.Value<int>();

        return new GrabItPosition(drive, value);
    }
}