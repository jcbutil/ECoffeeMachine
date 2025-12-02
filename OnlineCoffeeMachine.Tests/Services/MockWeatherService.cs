using System;
using System.Collections.Generic;
using System.Text;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Tests.Services
{
	public class MockWeatherService : IWeatherService
	{
		private readonly double mockTemp;
		private readonly double mockLatitude = 10.0;
		private readonly double mockLongitude = 10.0;

		public MockWeatherService(double temperature)
		{
			mockTemp = temperature;
		}

		public Task<double> GetCurrentTemperatureAsync(string userCity)
		{
			return Task.FromResult(mockTemp);
		}

		public Task<(double latitude, double longitude)> GetUserCityLocationAsync(string userCity)
		{
			return Task.FromResult((mockLatitude, mockLongitude));
		}
	}
}
