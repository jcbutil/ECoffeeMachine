using OnlineCoffeeMachine.Core;
using System.Threading;

namespace OnlineCoffeeMachine.Handler
{
	public class CoffeeMachineHandler : ICoffeeMachineHandler
	{
		private int brewCount = 0;
		private readonly IDateTimeProvider _dateTimeProvider;

		public CoffeeMachineHandler(IDateTimeProvider dateTimeProvider)
		{
			_dateTimeProvider = dateTimeProvider;
		}

		public Task<(int statusCode, object? response)> BrewCoffeeAsync()
		{
			// Ensure thread-safe increment of brewCount
			int currentBrewCount = Interlocked.Increment(ref brewCount);

			var dateTimeNow = _dateTimeProvider.UtcNow;

			// Check if current date is April 1st
			if (dateTimeNow.Month == 4 && dateTimeNow.Day == 1)
			{
				return Task.FromResult((418, (object?)null));
			}

			// Return 503 error every 5th call
			if (currentBrewCount % 5 == 0)
			{
				return Task.FromResult((503, (object?)null));
			}

			// Return normal response
			var response = new
			{
				message = "Your piping hot coffee is ready",
				prepared = dateTimeNow.ToString("yyyy-MM-ddTHH:mm:sszzz")
			};

			return Task.FromResult((200, (object?)response));
		}
	}
}