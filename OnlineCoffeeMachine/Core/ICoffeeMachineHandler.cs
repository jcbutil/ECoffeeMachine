namespace OnlineCoffeeMachine.Core
{
    public interface ICoffeeMachineHandler
    {
        Task<(int statusCode, object? response)> BrewCoffeeAsync();
    }
}
