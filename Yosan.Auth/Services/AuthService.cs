using Grpc.Core;
using Yosan.Auth.Contexts;
using Yosan.Auth.Services.ProtobufMethods;

namespace Yosan.Auth.Services;

public class AuthService(UserContext db) : Auth.AuthBase
{
    public override Task<SignInResponse> SignInUser(SignInRequest request, ServerCallContext context)
    {
        return new SignIn().AddUser(request, db);
    }

    public override Task<LogInResponse> LogInUser(LogInRequest request, ServerCallContext context)
    {
        return new LogIn().Authorize(request, db);
    }

    public override Task<TokenValidationResponse> ValidateToken(TokenValidationRequest request,
        ServerCallContext context)
    {
        return new Token().Validate(request);
    }

    public override Task<RefreshTokenResponse> GetAccessToken(RefreshTokenRequest request, ServerCallContext context)
    {
        return new Token().Get(request, db);
    }
}