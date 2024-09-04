using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yosan.Auth.Services
{
    public class TokenService
    {
        public JwtSecurityToken GetJwtToken(string username, int lifetime)
        {
            return new(issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: GetClaims(username).Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(lifetime)),
                signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        }

        private ClaimsIdentity GetClaims(string username)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usernameOrEmail)
            };
        
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private class AuthOptions
        {
            public const string Issuer = "Yosan.Auth";

            public const string Audience = "Yosan.Client";

            private const string Key = "mysupersecret_secretkey!123";

            public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
        }
    }
}