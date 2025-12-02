using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Controller
{
    [ApiController]
	[Route("CoffeeMachine")]
	public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineHandler _coffeeHandler;

        public CoffeeMachineController(ICoffeeMachineHandler coffeeHandler)
        {
            _coffeeHandler = coffeeHandler;
        }

		[HttpGet("/brew-coffee")]
		public async Task<IActionResult> BrewCoffee()
        {
            var (statusCode, response) = await _coffeeHandler.BrewCoffeeAsync();
            if (response == null)
            {
                Response.StatusCode = statusCode;
                return new EmptyResult();
            }

            return StatusCode(statusCode, response);
        }
    }
}
