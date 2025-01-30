namespace TestTasks.WeatherFromAPI.Models
{
    public class WeatherComparisonResult
    {
        public string CityA { get; init; }

        public string CityB { get; init; }

        public int WarmerDaysCount { get; init; }

        public int RainierDaysCount { get; init; }

        public WeatherComparisonResult(string cityA, string cityB, int warmerDays, int rainierDays)
        {
            CityA = cityA;
            CityB = cityB;
            WarmerDaysCount = warmerDays;
            RainierDaysCount = rainierDays;
        }
    }
}
