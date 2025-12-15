using Notification.Application.Interfaces;
using Notification.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<RabbitNotificationConsumer>();

var app = builder.Build();

app.MapGet("/", () => "Notification service running");

app.Run();
