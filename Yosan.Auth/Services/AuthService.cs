using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override Task<SignInRequest> SignInUser(SignInRequest request, ServerCallContext context)
        {

        }

        public override Task<LogInRequest> LogInUser(LogInRequest request, ServerCallContext context)
        {

        }

        public override Task<TokenValidationRequest> ValidateToken(TokenValidationRequest request, ServerCallContext context)
        {

        }

        public override Task<RefreshTokenService> RefreshAccessToken(RefreshTokenRequest request, ServerCallContext context)
        {
            
        }
    }
}