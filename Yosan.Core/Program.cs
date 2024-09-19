using Yosan.Core.Contexts;
using Yosan.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CoreContext>();

var app = builder.Build();

app.MapGrpcService<CoreService>();

app.Run();