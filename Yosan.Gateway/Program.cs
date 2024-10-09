using Yosan.Gateway.RabbitMq;
using Yosan.Gateway.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<RabbitMqListener>();

var app = builder.Build();

app.Run();