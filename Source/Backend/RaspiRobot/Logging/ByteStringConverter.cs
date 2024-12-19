namespace RaspiRobot.Logging;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Protobuf;

public class ByteStringConverter : JsonConverter<ByteString>
{
    public override ByteString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => throw new NotSupportedException("Deserialization of ByteString is not supported.");

    public override void Write(Utf8JsonWriter writer, ByteString value, JsonSerializerOptions options)
    {
        int size = value.Length;
        writer.WriteStringValue($"Byte Sequence (length: {size})");
    }
}