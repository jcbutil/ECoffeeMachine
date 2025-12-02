namespace OnlineCoffeeMachine.Core
{
	public interface IDateTimeService
	{
		// a read-only property that returns the current time
		DateTimeOffset UtcNow { get; }
	}
}
