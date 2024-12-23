using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Pim_Desktop.Models;

namespace Pim_Desktop
{
    public class WeatherService
    {
        private static readonly HttpClient client = new HttpClient();

        // Método para buscar a previsão do tempo com base na latitude e longitude
        public async Task<WeatherInfo?> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                string url = $"https://api.open-meteo.com/v1/forecast?latitude=-23.9608&longitude=-46.3336&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,precipitation,rain,showers,snowfall,weather_code,cloud_cover,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m&timezone=America%2FSao_Paulo";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                JObject weatherData = JObject.Parse(responseBody);

                var weatherInfo = new WeatherInfo
                {
                    Temperature = weatherData?["current"]?["temperature_2m"]?.ToString(),
                    WindSpeed = weatherData?["current"]?["wind_speed_10m"]?.ToString(),
                    Humidity = weatherData?["current"]?["relative_humidity_2m"]?.ToString(),
                    ApparentTemperature = weatherData?["current"]?["apparent_temperature"]?.ToString(),
                    WindDirection = weatherData?["current"]?["wind_direction_10m"]?.ToString(),
                    CloudCover = weatherData?["current"]?["cloud_cover"]?.ToString(),
                    Rain = weatherData?["current"]?["rain"]?.ToString()
                };

                return weatherInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}


