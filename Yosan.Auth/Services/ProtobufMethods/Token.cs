using System.IdentityModel.Tokens.Jwt;
using Yosan.Auth.Contexts;
using Yosan.Auth.Services.ValidationMethods;

namespace Yosan.Auth.Services.ProtobufMethods;

public class Token
{
    public async Task<TokenValidationResponse> Validate(TokenValidationRequest request)
    {
        AccessTokenValidator accessTokenValidator = new AccessTokenValidator();
        var validationResult = await accessTokenValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new TokenValidationResponse
                { IsSucceed = false, Error = string.Join(", ", validationResult.Errors) };
        }

        return new TokenValidationResponse { IsSucceed = true };
    }

    public async Task<RefreshTokenResponse> Get(RefreshTokenRequest request, UserContext db)
    {
        RefreshTokenValidator refreshTokenValidator = new RefreshTokenValidator();
        var validationResult = await refreshTokenValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new RefreshTokenResponse
                { IsSucceed = false, Error = string.Join(", ", validationResult.Errors) };
        }

        JwtSecurityToken jwt = new JwtSecurityToken(request.RefreshToken);

        return new RefreshTokenResponse
        {
            IsSucceed = true,
            AccessToken =
                new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(jwt.Claims.First().Value, 1)),
            RefreshToken =
                new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(jwt.Claims.First().Value, 180)),
            UserId = db.Users.FirstOrDefault(x => x.Username == jwt.Claims.First().Value)!
                .Id
                .ToString()
        };
    }
}