using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=47.4979&longitude=19.0402&current_weather=true";
            using HttpClient client = new();//Initialize the new http client to connect to the web

            try
            {
                var weatherData = await client.GetFromJsonAsync<WeatherReport>(url);

                if(weatherData != null)
                {
                    string[] weatherTime = weatherData.current_weather.time.Split('T');
                    string weatherDate = weatherTime[0];
                    string weatherDateClock = weatherTime[1];

                    Console.WriteLine($"Todays report: {weatherDate}");
                    Console.WriteLine($"Weather time: {weatherDateClock}");

                    Console.WriteLine($"Latitude: {weatherData.latitude}");
                    Console.WriteLine($"Longitude: {weatherData.longitude}");

                    Console.WriteLine($"Timezone: {weatherData.timezone}");
                    Console.WriteLine($"Temperature: {weatherData.current_weather.temperature}°C");
                    Console.WriteLine($"Windspeed: {weatherData.current_weather.windspeed}km/h");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class WeatherReport
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }

        public CurrentWeather current_weather { get; set; }
    }
    public class CurrentWeather
    {
        public string time { get; set; }
        public double temperature { get; set; }
        public double windspeed { get; set; }
    }
}
