using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class WeatherDataModel
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
        [JsonPropertyName("rain")]
        public double Rain { get; set; } 
    }
}
