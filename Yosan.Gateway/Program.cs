using Yosan.Gateway.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

new RouterService().Execute(app);

app.Run();