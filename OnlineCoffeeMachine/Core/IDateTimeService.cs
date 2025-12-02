namespace OnlineCoffeeMachine.Core
{
	public interface IDateTimeService
	{
		DateTimeOffset UtcNow { get; }
	}
}
