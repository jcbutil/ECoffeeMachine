using System;
using System.Collections.Generic;
using System.Text;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Tests.Services
{
	public class MockWeatherService : IWeatherService
	{
		private readonly double _temperature;

		public MockWeatherService(double temperature)
		{
			_temperature = temperature;
		}

		public Task<double> GetCurrentTemperatureAsync()
		{
			return Task.FromResult(_temperature);
		}
	}
}
