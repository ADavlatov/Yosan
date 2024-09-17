namespace Yosan.Gateway.Services;

public class RouterService(WebApplication app)
{
    public async Task Execute()
    {
        app.MapPost("/api/v1/auth/signIn", () =>
        {

        });

        app.MapPost("api/v1/auth/logIn", () =>
        {
            
        });
        
        app.MapPost("api/v1/auth/logIn", () =>
        {
            
        });
    }
}