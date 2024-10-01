using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Yosan.Auth.Services;

public class TokenService
{
    // Генерация JWT токена
    public static JwtSecurityToken GetJwtToken(string userId, int lifetime)
    {
        return new(issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: GetClaims(userId)
                .Claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(lifetime)),
            signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    }

    //Получение claims для генерации JWT токена
    private static ClaimsIdentity GetClaims(string userId)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userId)
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

        const string Key = "mysupersecret_secretkey!123";

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
    }
}