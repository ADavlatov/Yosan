using GridSystem.Web.Server.Auth.Contexts;
using GridSystem.Web.Server.Auth.Services.ProtobufMethods;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace GridSystem.Web.Server.Auth.Services;

public class AuthService(UserContext db) : Auth.AuthBase
{
    public override async Task<SignInResponse> SignInUser(SignInRequest request, ServerCallContext context)
    {
        return await new SignIn().AddUser(request, db);
    }

    public override async Task<LogInResponse> LogInUser(LogInRequest request, ServerCallContext context)
    {
        return await new LogIn().Authorize(request, db);
    }

    public override async Task<TokenValidationResponse> ValidateToken(TokenValidationRequest request,
        ServerCallContext context)
    {
        return await new Token(db).Validate(request);
    }

    public override async Task<RefreshTokenResponse> GetAccessToken(RefreshTokenRequest request, ServerCallContext context)
    {
        return await new Token(db).Get(request, db);
    }
}