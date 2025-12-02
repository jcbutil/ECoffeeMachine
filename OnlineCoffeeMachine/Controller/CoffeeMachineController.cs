using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeMachine.Handler.Interface;

namespace OnlineCoffeeMachine.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly ICoffeeMachineHandler _coffeeService;

        public CoffeeMachineController(ICoffeeMachineHandler coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet("brew-coffee")]
        public async Task<IActionResult> BrewCoffee()
        {
            var (statusCode, response) = await _coffeeService.BrewCoffeeAsync();
            if (response == null)
                return StatusCode(statusCode);

            return StatusCode(statusCode, response);
        }
    }
}
