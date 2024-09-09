using Grpc.Core;
using Yosan.Auth.Contexts;
using Yosan.Auth.Services.ProtobufMethods;

namespace Yosan.Auth.Services
{
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

        public override async Task<TokenValidationResponse> ValidateToken(TokenValidationRequest request, ServerCallContext context)
        {
            return await new Token().Validate(request, db);
        }

        public override async Task<RefreshTokenResponse> RefreshAccessToken(RefreshTokenRequest request, ServerCallContext context)
        {
            return await new Token().Resfresh(request, db);
        }
    }
}