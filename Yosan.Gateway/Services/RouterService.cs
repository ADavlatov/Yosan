using Grpc.Net.Client;

namespace Yosan.Gateway.Services;

public class RouterService
{
    private readonly Auth.AuthClient _authClient;

    public RouterService()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7231");
        _authClient = new Auth.AuthClient(channel);
    }

    public void Execute(WebApplication app)
    {
        app.MapPost("/api/v1/auth/signIn",
            async (SignInRequest request) => await _authClient.SignInUserAsync(request));

        app.MapPost("api/v1/auth/logIn",
            async (LogInRequest request) => await _authClient.LogInUserAsync(request));

        app.MapPost("api/v1/auth/validate",
            async (TokenValidationRequest request) => await _authClient.ValidateTokenAsync(request));

        app.MapPost("api/v1/auth/refresh",
            async (RefreshTokenRequest request) => await _authClient.GetAccessTokenAsync(request));
    }
}