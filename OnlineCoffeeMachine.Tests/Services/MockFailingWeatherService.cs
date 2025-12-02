using System;
using System.Collections.Generic;
using System.Text;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Tests.Services
{
	public class MockFailingWeatherService : IWeatherService
	{
		public Task<(double latitude, double longitude)> GetUserCityLocationAsync(string userCity)
		{
			throw new HttpRequestException("Failed to retrieve weather.");
		}

		public Task<double> GetCurrentTemperatureAsync(string userCity)
		{
			throw new HttpRequestException("Failed to retrieve weather.");
		}
	}
}
