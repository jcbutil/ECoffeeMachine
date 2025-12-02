using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeMachine.Handler.Interface;

namespace OnlineCoffeeMachine.Controller
{
    [ApiController]
	[Route("CoffeeMachine")]
	public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineHandler _coffeeService;

        public CoffeeMachineController(ICoffeeMachineHandler coffeeService)
        {
            _coffeeService = coffeeService;
        }

		[HttpGet("/brew-coffee")]
		public async Task<IActionResult> BrewCoffee()
        {
            var (statusCode, response) = await _coffeeService.BrewCoffeeAsync();
            if (response == null)
            {
                Response.StatusCode = statusCode;
                return new EmptyResult();
            }

            return StatusCode(statusCode, response);
        }
    }
}
