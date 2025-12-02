using OnlineCoffeeMachine.Handler;
using Xunit;
using System.Threading.Tasks;
using OnlineCoffeeMachine.Tests.Services;


namespace OnlineCoffeeMachine.Tests.Tests.Handler
{
	public class CoffeeMachineServiceTests
	{
		// For Non-April 1st scenarios
		private readonly MockDateTimeService _normalDateProvider = new(2025, 12, 2);

		[Fact]
		public async Task BrewCoffee_HappyFlow_Returns200()
		{
			// ARRANGE: Inject the normal date provider
			var service = new CoffeeMachineHandler(_normalDateProvider);

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync();

			// ASSERT
			Assert.Equal(200, statusCode);
			Assert.NotNull(response);
		}

		[Fact]
		public async Task BrewCoffee_FifthCall_Returns503()
		{
			// ARRANGE
			var service = new CoffeeMachineHandler(_normalDateProvider);

			for (int i = 0; i < 4; i++)
			{
				await service.BrewCoffeeAsync();
			}

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync();

			// ASSERT
			Assert.Equal(503, statusCode);
			Assert.Null(response);
		}

		[Fact]
		public async Task BrewCoffee_AprilFirst_Return418Teapot()
		{
			// ARRANGE: Set date to April 1st
			var aprilFirstProvider = new MockDateTimeService(2025, 4, 1);
			var service = new CoffeeMachineHandler(aprilFirstProvider);

			// ACT
			var (statusCode, response) = await service.BrewCoffeeAsync();

			// ASSERT
			Assert.Equal(418, statusCode);
			Assert.Null(response);
		}
	}
}