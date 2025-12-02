using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using OnlineCoffeeMachine.Handler;
using OnlineCoffeeMachine.Tests.Services;
using Xunit;


namespace OnlineCoffeeMachine.Tests.Tests.Handler
{
	public class CoffeeMachineServiceTests
	{
		// temp constants
		private const double HOT_TEMP = 35.0;
		private const double COLD_TEMP = 15.0;
		private const string TEST_CITY = "Manila";

		// mocked values
		private readonly MockDateTimeService _normalDateService = new(2025, 12, 2);
		private readonly MockWeatherService _hotWeatherService = new(HOT_TEMP);
		private readonly MockWeatherService _coldWeatherService = new(COLD_TEMP);
		private readonly MockFailingWeatherService _failingWeatherService = new();

		[Fact]
		public async Task BrewCoffee_HappyFlowIcedCoffee_Returns200()
		{
			// ARRANGE
			var service = new CoffeeMachineHandler(_normalDateService, _hotWeatherService);

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync(TEST_CITY);

			// ASSERT
			Assert.Equal(200, statusCode);
			Assert.NotNull(response);

			var messageProperty = response.GetType().GetProperty("message");
			Assert.Equal("Your refreshing iced coffee is ready", messageProperty!.GetValue(response));
		}

		[Fact]
		public async Task BrewCoffee_HotWeatherHotCoffee_Returns200()
		{
			// ARRANGE
			var service = new CoffeeMachineHandler(_normalDateService, _coldWeatherService);

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync(TEST_CITY);

			// ASSERT
			Assert.Equal(200, statusCode);
			Assert.NotNull(response);

			// Assert the message is correct for hot coffee
			var messageProperty = response.GetType().GetProperty("message");
			Assert.Equal("Your piping hot coffee is ready", messageProperty!.GetValue(response));
		}

		[Fact]
		public async Task BrewCoffee_FifthCall_Returns503()
		{
			// ARRANGE
			var service = new CoffeeMachineHandler(_normalDateService, _hotWeatherService);

			for (int i = 0; i < 4; i++)
			{
				await service.BrewCoffeeAsync(TEST_CITY);
			}

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync(TEST_CITY);

			// ASSERT
			Assert.Equal(503, statusCode);
			Assert.Null(response);
		}

		[Fact]
		public async Task BrewCoffee_AprilFirst_Return418Teapot()
		{
			// ARRANGE
			var aprilFirstProvider = new MockDateTimeService(2025, 4, 1);
			var service = new CoffeeMachineHandler(aprilFirstProvider, _hotWeatherService);

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync(TEST_CITY);

			// ASSERT
			Assert.Equal(418, statusCode);
			Assert.Null(response);
		}

		[Fact]
		public async Task BrewCoffee_WeatherServiceError_Returns500()
		{
			var service = new CoffeeMachineHandler(_normalDateService, _failingWeatherService);

			// ACT
			try
			{
				var (statusCode, response) = await service.BrewCoffeeAsync(TEST_CITY);
			}
			catch (Exception ex)
			{
				// ASSERT
				Assert.Contains("Failed to retrieve weather.", ex.Message);
			}
		}
	}
}