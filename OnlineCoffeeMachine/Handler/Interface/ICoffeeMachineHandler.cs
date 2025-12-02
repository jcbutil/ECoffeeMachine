namespace OnlineCoffeeMachine.Handler.Interface
{
    public interface ICoffeeMachineHandler
    {
        Task<(int statusCode, object? response)> BrewCoffeeAsync();
    }
}
