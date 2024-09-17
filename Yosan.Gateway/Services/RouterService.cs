using Grpc.Net.Client;

namespace Yosan.Gateway.Services;

public class RouterService
{
    private readonly Auth.AuthClient _authClient;
    public RouterService()
    {
        var channel = GrpcChannel.ForAddress("");
        _authClient = new Auth.AuthClient(channel);
    }
    public async Task Execute(WebApplication app)
    {
        app.MapPost("/api/v1/auth/signIn", (SignInRequest request) =>
        {
            
        });

        app.MapPost("api/v1/auth/logIn", (LogInRequest request) =>
        {
            
        });
        
        app.MapPost("api/v1/auth/validate", (TokenValidationRequest request) =>
        {
            
        });
        
        app.MapPost("api/v1/auth/refresh", (RefreshTokenRequest request) =>
        {
            
        });
    }
}