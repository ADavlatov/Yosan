using System.IdentityModel.Tokens.Jwt;
using Yosan.Auth.Contexts;
using Yosan.Auth.Services.ValidationMethods;

namespace Yosan.Auth.Services.ProtobufMethods;

public class Token
{
    //TODO добавить проверку на userID
    public async Task<TokenValidationResponse> Validate(TokenValidationRequest request)
    {
        return new TokenValidationResponse { IsSucceed = true };
    }


    //TODO сделать проверку userID 
    public async Task<RefreshTokenResponse> Get(RefreshTokenRequest request, UserContext db)
    {
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