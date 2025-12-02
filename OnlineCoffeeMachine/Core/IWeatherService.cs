namespace OnlineCoffeeMachine.Core
{
    public interface IWeatherService
    {
		Task<double> GetCurrentTemperatureAsync(string userCity);
		Task<(double latitude, double longitude)> GetUserCityLocationAsync(string userCity);
	}
}
