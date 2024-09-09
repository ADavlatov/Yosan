using Yosan.Auth.Contexts;
using Yosan.Auth.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AuthService>();

app.Run();