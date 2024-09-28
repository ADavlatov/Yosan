using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Yosan.Auth.Contexts;
using Yosan.Auth.Services.ProtobufMethods;

namespace Yosan.Auth.Services;

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
        return await new Token().Validate(request);
    }

    public override async Task<RefreshTokenResponse> GetAccessToken(RefreshTokenRequest request, ServerCallContext context)
    {
        return await new Token().Get(request, db);
    }

    public override async Task<CheckUserResponse> CheckUser(CheckUserRequest request, ServerCallContext context)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id.ToString() == request.UserId);

        if (user == null)
        {
            return new CheckUserResponse { IsSucceed = false, Status = 400, Error = "User does not exist" };
        }
        
        return new CheckUserResponse { IsSucceed = true, Status = 200, Error = "" };
    }
}