namespace RaspiRobot.RobotControl.GrabIt.Settings;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RaspiRobot.RobotControl.Settings;

internal class GrabItPositionConverter : JsonConverter<IPosition>
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
        JObject jsonObject = JObject.Load(reader);
        var drive = jsonObject["Drive"]!.Value<byte>();
        var value = jsonObject["Value"]!.Value<int>();

        return new GrabItPosition(drive, value);
    }
}