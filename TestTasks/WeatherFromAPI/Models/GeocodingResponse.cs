using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class GeocodingResponse
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
    }
}
