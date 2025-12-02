namespace OnlineCoffeeMachine.Core
{
	public interface IDateTimeProvider
	{
		// a read-only property that returns the current time
		DateTimeOffset UtcNow { get; }
	}
}
