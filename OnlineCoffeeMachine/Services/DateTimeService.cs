using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Services
{
	public class DateTimeService : IDateTimeService
	{
		// returns the current time, including the time zone offset.
		public DateTimeOffset UtcNow => DateTimeOffset.Now;
	}
}
