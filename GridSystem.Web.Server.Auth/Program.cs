using GridSystem.Web.Server.Auth.Contexts;
using GridSystem.Web.Server.Auth.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<AuthService>();

app.Run();