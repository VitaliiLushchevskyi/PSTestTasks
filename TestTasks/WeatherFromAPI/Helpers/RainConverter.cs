using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Helpers
{
    public class RainConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                reader.Read(); 
                reader.Read();
                double rainValue = reader.GetDouble();
                reader.Read();
                return rainValue;
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble();
            }

            return 0.0; 
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
