using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Services
{
	public class DateTimeService : IDateTimeProvider
	{
		// returns the current time, including the time zone offset.
		public DateTimeOffset UtcNow => DateTimeOffset.Now;
	}
}
