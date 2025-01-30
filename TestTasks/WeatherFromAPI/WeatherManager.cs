using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestTasks.WeatherFromAPI.Helpers;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    public class WeatherManager
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "0eeec1eb1f8d17fa7af099784c36c2f1";
        private const string GeocodingUrl = "http://api.openweathermap.org/geo/1.0/direct?q={0}&limit=1&appid=" + ApiKey;
        private const string OneCallUrl = "https://api.openweathermap.org/data/3.0/onecall?lat={0}&lon={1}&dt={2}&appid=" + ApiKey;

        public WeatherManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherComparisonResult> CompareWeather(string cityA, string cityB, int dayCount)
        {
            if (dayCount < 1 || dayCount > 5)
                throw new ArgumentException("Day count must be between 1 and 5.");

            var locationA = await GetCityCoordinates(cityA);
            var locationB = await GetCityCoordinates(cityB);

            var todayMiddayUtc = new DateTimeOffset(DateTime.UtcNow.Date.AddHours(12)).ToUnixTimeSeconds();

            int warmerDays = 0;
            int rainierDays = 0;

            for (int i = 0; i < dayCount; i++)
            {
                long targetTimestamp = todayMiddayUtc - (i * 86400);

                var weatherA = await GetWeatherData(locationA, targetTimestamp);
                var weatherB = await GetWeatherData(locationB, targetTimestamp);

                double avgTempA = weatherA.Average(w => w.Temp);
                double avgTempB = weatherB.Average(w => w.Temp);
                double totalRainA = weatherA.Sum(w => w.Rain);
                double totalRainB = weatherB.Sum(w => w.Rain);

                if (avgTempA > avgTempB) warmerDays++;
                if (totalRainA > totalRainB) rainierDays++;
            }

            return new WeatherComparisonResult(cityA, cityB, warmerDays, rainierDays);
        }

        private async Task<(double Lat, double Lon)> GetCityCoordinates(string city)
        {
            string url = string.Format(GeocodingUrl, city);
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new ArgumentException($"Failed to fetch coordinates for city: {city}");

            var json = await response.Content.ReadAsStringAsync();
            var locations = JsonSerializer.Deserialize<List<GeocodingResponse>>(json);

            if (locations == null || locations.Count == 0)
                throw new ArgumentException($"City '{city}' not found.");

            var location = locations.First();
            return (location.Lat, location.Lon);
        }

        private async Task<List<WeatherDataModel>> GetWeatherData((double Lat, double Lon) location, long timestamp)
        {
            string url = string.Format(OneCallUrl, location.Lat, location.Lon, timestamp);
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch weather data.");

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                Converters = { new RainConverter() }
            };

            var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(json, options);

            return weatherResponse?.HourlyData ?? throw new Exception("Invalid weather data received.");
        }
    }
}
