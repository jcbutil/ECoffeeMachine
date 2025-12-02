namespace OnlineCoffeeMachine.Core
{
    public interface IWeatherService
    {
		Task<double> GetCurrentTemperatureAsync();
	}
}
