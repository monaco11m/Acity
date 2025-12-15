using MassLoad.Application.Interfaces;
using MassLoad.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IMassLoadProcessor, MassLoadProcessor>();
builder.Services.AddHostedService<RabbitConsumerService>();
builder.Services.AddScoped<IRabbitPublisher, RabbitMqPublisher>();



var app = builder.Build();

app.MapGet("/", () => "MassLoad service running");

app.Run();
