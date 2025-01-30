using System.Text.Json.Serialization;
using TestTasks.WeatherFromAPI.Helpers;

namespace TestTasks.WeatherFromAPI.Models
{
    public class WeatherDataModel
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
        [JsonPropertyName("rain")]
        [JsonConverter(typeof(RainConverter))]
        public double Rain { get; set; } 
    }
}
