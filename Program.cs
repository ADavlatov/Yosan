var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Services.AddDbContext<UserContext>();
app.MapGrpcService<AuthService>();

app.Run();