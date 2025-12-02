using OnlineCoffeeMachine.Handler.Interface;
using OnlineCoffeeMachine.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<ICoffeeMachineHandler, CoffeeMachineHandler>();

app.Run();


