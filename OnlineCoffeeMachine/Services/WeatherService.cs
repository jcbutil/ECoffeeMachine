using System.Text.Json;
using Microsoft.Extensions.Configuration;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Services
{
    public class WeatherService : IWeatherService
    {
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public WeatherService(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_apiKey = configuration.GetValue<string>("OPEN_WEATHER_MAP_API_KEY")
				 ?? throw new InvalidOperationException("OPEN_WEATHER_MAP_API_KEY not found in configuration.");
		}

		public async Task<double> GetCurrentTemperatureAsync(string userCity)
		{
			long time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			// get user city coordinates
			(double Latitude, double Longitude) = await GetUserCityLocationAsync(userCity);

			// OpenWeatherMap API to get location weather using metric unit
			string url = $"https://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longitude}&appid={_apiKey}&units=metric";

			try
			{
				using var response = await _httpClient.GetAsync(url);

				if (!response.IsSuccessStatusCode)
				{
					throw new HttpRequestException($"Failed to retrieve weather. Status: {response.StatusCode}");
				}
				using var contentStream = await response.Content.ReadAsStreamAsync();

				using (var doc = await JsonDocument.ParseAsync(contentStream))
				{
					if (doc.RootElement.TryGetProperty("main", out JsonElement mainData) && 
						mainData.TryGetProperty("temp", out JsonElement tempElement))
					{
						return tempElement.GetDouble();
					}
					else
					{
						throw new HttpRequestException($"Failed to retrieve weather. Status: {response.StatusCode}");
					}
				}
			}
			catch (Exception)
			{
				throw new HttpRequestException($"Failed to retrieve weather.");
			}
		}

		public async Task<(double latitude, double longitude)> GetUserCityLocationAsync(string userCity)
		{
			// OpenWeatherMap API to get city coordinates
			string url = $"https://api.openweathermap.org/geo/1.0/direct?q={userCity}&limit=1&appid={_apiKey}";

			using var response = await _httpClient.GetAsync(url);

			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException($"Failed to retrieve user location.");
			}

			using var contentStream = await response.Content.ReadAsStreamAsync();

			try
			{
				using (var doc = await JsonDocument.ParseAsync(contentStream))
				{
					JsonElement root = doc.RootElement;

					if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
					{
						JsonElement firstResult = root[0];

						if (firstResult.TryGetProperty("lat", out JsonElement latElement) &&
							firstResult.TryGetProperty("lon", out JsonElement lonElement))
						{
							return (latElement.GetDouble(), lonElement.GetDouble());
						}
						else
						{
							throw new HttpRequestException($"Failed to retrieve user location.");
						}
					}
					else
					{
						throw new HttpRequestException($"Failed to retrieve user location.");
					}
				}
			}
			catch (Exception)
			{
				throw new HttpRequestException($"Failed to retrieve user location.");
			}
		}
	}
}
