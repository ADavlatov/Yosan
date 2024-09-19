using Grpc.Net.Client;
using Yosan.Gateway.Services.Validation;

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
            async (SignInRequest request) =>
            {
                var result = await new SignInValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new SignInResponse { IsSucceed = false, Errors = string.Join(", ", result.Errors) };
                }

                return await _authClient.SignInUserAsync(request);
            });


        app.MapPost("api/v1/auth/logIn",
            async (LogInRequest request) =>
            {
                var result = await new LogInValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new LogInResponse { IsSucceed = false, Errors = string.Join(", ", result.Errors) };
                }

                return await _authClient.LogInUserAsync(request);
            });

        app.MapPost("api/v1/auth/validate",
            async (TokenValidationRequest request) =>
            {
                var result = await new AccessTokenValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new TokenValidationResponse { IsSucceed = false, Errors = string.Join(", ", result.Errors) };
                }

                return await _authClient.ValidateTokenAsync(request);
            });

        app.MapPost("api/v1/auth/refresh",
            async (RefreshTokenRequest request) =>
            {
                var result = await new RefreshTokenValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new RefreshTokenResponse { IsSucceed = false, Errors = string.Join(", ", result.Errors) };
                }

                return await _authClient.GetAccessTokenAsync(request);
            });
    }
}