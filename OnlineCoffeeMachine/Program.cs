using OnlineCoffeeMachine.Core;
using OnlineCoffeeMachine.Handler;
using OnlineCoffeeMachine.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
builder.Services.AddSingleton<ICoffeeMachineHandler, CoffeeMachineHandler>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


