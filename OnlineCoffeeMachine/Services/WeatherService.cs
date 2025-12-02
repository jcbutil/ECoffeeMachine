using System.Text.Json;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Services
{
    public class WeatherService : IWeatherService
    {
		private readonly HttpClient _httpClient;
		private const string ApiKey = "8fcb771fa8b253af74f82e062a098315"; 
		private const double Latitude = 14.0000; // placeholder only for high temp location
		private const double Longitude = 32.7167; // placeholder only for high temp location

		private const double HotTemperatureThreshold = 30.0;

		public WeatherService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<double> GetCurrentTemperatureAsync()
		{
			long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			// OpenWeatherMap URL using metric units
			string url = $"https://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longitude}&appid={ApiKey}&units=metric";

			try
			{
				using var response = await _httpClient.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					return HotTemperatureThreshold - 1;
				}
				using var contentStream = await response.Content.ReadAsStreamAsync();

				using (var doc = await JsonDocument.ParseAsync(contentStream))
				{
					if (doc.RootElement.TryGetProperty("main", out JsonElement mainData) && 
						mainData.TryGetProperty("temp", out JsonElement tempElement))
					{
						return tempElement.GetDouble();
					}
				}
			}
			catch (Exception e)
			{
				var error = e.Data;
				return HotTemperatureThreshold - 1.0;
			}

			return HotTemperatureThreshold - 1.0;
		}
	}
}
