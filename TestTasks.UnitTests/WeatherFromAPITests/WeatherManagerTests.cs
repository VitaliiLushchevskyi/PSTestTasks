using TestTasks.WeatherFromAPI;
using TestTasks.UnitTests.Helpers;

namespace TestTasks.UnitTests.WeatherFromAPITests
{
    public class WeatherManagerTests
    {
        [Fact]
        public async Task CompareWeather_ReturnsCorrectComparison()
        {
            var todayMiddayUtc = new DateTimeOffset(DateTime.UtcNow.Date.AddHours(12)).ToUnixTimeSeconds();

            // Mock API responses 
            var mockResponses = new Dictionary<string, string>
            {
                { $"http://api.openweathermap.org/geo/1.0/direct?q=CityA&limit=1&appid=0eeec1eb1f8d17fa7af099784c36c2f1", "[{\"lat\":40.7128,\"lon\":-74.0061}]" },
                { $"http://api.openweathermap.org/geo/1.0/direct?q=CityB&limit=1&appid=0eeec1eb1f8d17fa7af099784c36c2f1", "[{\"lat\":34.0522,\"lon\":-118.2437}]" },
                { $"https://api.openweathermap.org/data/3.0/onecall/?lat=40.7128&lon=-74.0061&dt={todayMiddayUtc}&appid=0eeec1eb1f8d17fa7af099784c36c2f1", "{\"data\":[{\"temp\":20.5,\"rain\":1.2}]}" },
                { $"https://api.openweathermap.org/data/3.0/onecall/?lat=34.0522&lon=-118.2437&dt={todayMiddayUtc}&appid=0eeec1eb1f8d17fa7af099784c36c2f1", "{\"data\":[{\"temp\":22.1,\"rain\":0.0}]}" }
            };

            var httpClient = HttpClientMockHelper.CreateMockHttpClient(mockResponses);
            var weatherManager = new WeatherManager(httpClient);

            // Act
            var result = await weatherManager.CompareWeather("CityA", "CityB", 1);

            // Assert
            Assert.Equal("CityA", result.CityA);
            Assert.Equal("CityB", result.CityB);
            Assert.Equal(0, result.WarmerDaysCount);
            Assert.Equal(1, result.RainierDaysCount);
        }

        [Fact]
        public async Task CompareWeather_ThrowsException_WhenDayCountExceedsLimit()
        {
            // Arrange
            var httpClient = HttpClientMockHelper.CreateMockHttpClient([]);
            var weatherManager = new WeatherManager(httpClient);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await weatherManager.CompareWeather("CityA", "CityB", 6));

            Assert.Equal("Day count must be between 1 and 5.", exception.Message);
        }
    }
}
