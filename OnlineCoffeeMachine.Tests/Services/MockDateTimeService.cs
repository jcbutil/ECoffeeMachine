using System;
using System.Collections.Generic;
using System.Text;
using OnlineCoffeeMachine.Core;

namespace OnlineCoffeeMachine.Tests.Services
{
    public class MockDateTimeService : IDateTimeService
    {
        private readonly DateTimeOffset _fixedDate;

        public MockDateTimeService(int year, int month, int day) =>
            _fixedDate = new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero);

        public DateTimeOffset UtcNow => _fixedDate;
    }
}
