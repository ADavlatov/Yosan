using Yosan.Gateway.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

await new RouterService().Execute(app);

app.Run();