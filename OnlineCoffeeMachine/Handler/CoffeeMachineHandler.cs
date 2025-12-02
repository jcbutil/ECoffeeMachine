
using OnlineCoffeeMachine.Handler.Interface;
namespace OnlineCoffeeMachine.Handler
{
    public class CoffeeMachineHandler : ICoffeeMachineHandler
    {
        private int brewCount = 0;
        private readonly object locker = new();
        public Task<(int statusCode, object? response)> BrewCoffeeAsync()
        {
            lock (locker)
            {
                brewCount++;
                var dateTimeNow = DateTimeOffset.Now;

                // Check if current date is April 1st
                if (dateTimeNow.Month == 4 && dateTimeNow.Day == 1)
                    return Task.FromResult((418, (object?)null));

                // Return 503 error every 5th call
                if (brewCount % 5 == 0)
                    return Task.FromResult((503, (object?)null));

                // Return normal response
                var response = new
                {
                    message = "Your piping hot coffee is ready",
                    prepared = dateTimeNow.ToString("yyyy-MM-ddTHH:mm:sszzz")
                };

                return Task.FromResult((200, (object?)response));
            }
        }
    }
}
