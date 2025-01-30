using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TestTasks.InternationalTradeTask;
using TestTasks.VowelCounting;
using TestTasks.WeatherFromAPI;

namespace TestTasks
{
    class Program
    {
        static async Task Main()
        {
            //Below are examples of usage. However, it is not guaranteed that your implementation will be tested on those examples.

            //first task usage
            var stringProcessor = new StringProcessor();
            string str = File.ReadAllText("./CharCounting/StringExample.txt");
            var charCount = stringProcessor.GetCharCount(str, new char[] { 'l', 'r', 'm' });

            foreach (var (symbol, count) in charCount)
            {
                Console.WriteLine($"Character '{symbol}': {count}");
            }

            //second task usage
            var commodityRepository = new CommodityRepository();
            Console.WriteLine(commodityRepository.GetImportTariff("Sugar, sugar preparations and honey"));
            Console.WriteLine(commodityRepository.GetExportTariff("Sugar, sugar preparations and honey"));

            //third task usage
            var httpClient = new HttpClient();
            var weatherManager = new WeatherManager(httpClient);
            var comparisonResult = await weatherManager.CompareWeather("kyiv,ua", "lviv,ua", 4);
            Console.WriteLine($"Number of warmer days in {comparisonResult.CityA}: {comparisonResult.WarmerDaysCount}");
            Console.WriteLine($"Number of rainier days in {comparisonResult.CityA}: {comparisonResult.RainierDaysCount}");
        }
    }
}
