namespace RaspiRobot.RobotControl.Settings;

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class StepSettingsConverter : JsonConverter<IStepSettings>
{
    public override void WriteJson(
        JsonWriter writer,
        IStepSettings? value,
        JsonSerializer serializer)
    {
        switch (value)
        {
            case MoveStepSettings moveStepSettings:
                serializer.Serialize(writer, new { Type = "MoveStepSettings", moveStepSettings.Positions });
                break;
            case ChuckLoadingChangedNotificationStepSettings:
                serializer.Serialize(writer, new { Type = "ChuckLoadingChangedNotificationStepSettings" });
                break;
            default:
                throw new NotSupportedException($"Serialization of type '{value!.GetType()}'is not supported.");
        }
    }

    public override IStepSettings? ReadJson(
        JsonReader reader,
        Type objectType,
        IStepSettings? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        IStepSettings? stepSettings = null;
        JObject jsonObject = JObject.Load(reader);

        stepSettings = (jsonObject["Type"]?.Value<string>() ?? "MoveStepSettings") switch
        {
            "MoveStepSettings" => new MoveStepSettings(),
            "ChuckLoadingChangedNotificationStepSettings" => new ChuckLoadingChangedNotificationStepSettings(),
            _ => throw new NotSupportedException($"Deserialization of type '{jsonObject["Type"]}'is not supported."),
        };

        serializer.Populate(jsonObject.CreateReader(), stepSettings);
        return stepSettings;
    }
}