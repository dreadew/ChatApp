using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatApp.Core.Json;

public class JsonIgnoreZeroConverter : JsonConverter<int>
{
  public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
    reader.GetInt32();

  public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
  {
    if (value != 0)
    {
      writer.WriteNumberValue(value);
    }
  }
}