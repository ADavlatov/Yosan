using Yosan.Auth.Contexts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Services.AddDbContext<UserContext>();

app.Run();