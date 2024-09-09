using Grpc.Core;
using Yosan.Auth.Contexts;

namespace Yosan.Auth.Services
{
    public class AuthService : Auth.AuthBase
    {
        private readonly UserContext _db;
        public AuthService(UserContext db)
        {
            _db = db;
        }

        public override Task<SignInResponse> SignInUser(SignInRequest request, ServerCallContext context)
        {
            return base.SignInUser(request, context);
        }

        public override Task<LogInResponse> LogInUser(LogInRequest request, ServerCallContext context)
        {
            return base.LogInUser(request, context);
        }

        public override Task<TokenValidationResponse> ValidateToken(TokenValidationRequest request, ServerCallContext context)
        {
            return base.ValidateToken(request, context);
        }

        public override Task<RefreshTokenResponse> RefreshAccessToken(RefreshTokenRequest request, ServerCallContext context)
        {
            return base.RefreshAccessToken(request, context);
        }
    }
}