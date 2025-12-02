using System.Threading; // Important: You need this namespace for Interlocked
using OnlineCoffeeMachine.Core;
using OnlineCoffeeMachine.Services;

namespace OnlineCoffeeMachine.Handler
{
	public class CoffeeMachineHandler : ICoffeeMachineHandler
	{
		private int brewCount = 0;
		private const double hotTemperatureThreshold = 30.0;
		private readonly IDateTimeService _dateTimeProvider;
		private readonly IWeatherService _weatherService;

		public CoffeeMachineHandler(IDateTimeService dateTimeService, IWeatherService weatherService)
		{
			_dateTimeProvider = dateTimeService;
			_weatherService = weatherService;
		}

		public async Task<(int statusCode, object? response)> BrewCoffeeAsync(string userCity)
		{
			try 
			{
				// Ensure thread-safe increment of brewCount
				int currentBrewCount = Interlocked.Increment(ref brewCount);

				var dateTimeNow = _dateTimeProvider.UtcNow;

				// Check if current date is April 1st
				if (dateTimeNow.Month == 4 && dateTimeNow.Day == 1)
				{
					return (418, (object?)null);
				}

				// Return 503 error every 5th call
				if (currentBrewCount % 5 == 0)
				{
					return (503, (object?)null);
				}

				// Get current temperature for the user's city
				double currentTemp = await _weatherService.GetCurrentTemperatureAsync(userCity);

				string coffeeMessage;
				if (currentTemp > hotTemperatureThreshold)
				{
					coffeeMessage = "Your refreshing iced coffee is ready";
				}
				else
				{
					coffeeMessage = "Your piping hot coffee is ready";
				}

				// Return normal response
				var response = new
				{
					message = coffeeMessage,
					prepared = dateTimeNow.ToString("yyyy-MM-ddTHH:mm:sszzz")
				};

				return (200, (object?)response);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}