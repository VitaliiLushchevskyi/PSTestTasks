using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestTasks.WeatherFromAPI.Models
{
    public class WeatherApiResponse
    {
        [JsonPropertyName("hourly")]
        public List<WeatherDataModel> HourlyData { get; set; }
    }
}
