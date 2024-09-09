using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yosan.Auth.Contexts;

namespace Yosan.Auth.Services.ProtobufMethods
{
    public class Token
    {
        public async Task<TokenValidationResponse> Validate(TokenValidationRequest request, UserContext db)
        {
            return null;
        }

        public async Task<RefreshTokenResponse> Resfresh(RefreshTokenRequest request, UserContext db)
        {
            return null;
        }
    }
}