using System.IdentityModel.Tokens.Jwt;
using GridSystem.Web.Server.Auth.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GridSystem.Web.Server.Auth.Services.ProtobufMethods;

public class Token(UserContext db)
{
    public async Task<TokenValidationResponse> Validate(TokenValidationRequest request)
    {
        var result = await IsWrongToken(request.AccessToken);

        if (result ==  "Invalid token.")
        {
            return new TokenValidationResponse { IsSucceed = false, Status = 400, Error = result };
        }

        return new TokenValidationResponse { IsSucceed = true, Status = 200, Error = "" };
    }

    public async Task<RefreshTokenResponse> Get(RefreshTokenRequest request, UserContext db)
    {
        var result = await IsWrongToken(request.RefreshToken);

        if (result ==  "Invalid token.")
        {
            return new RefreshTokenResponse { IsSucceed = false, Status = 400, Error = result };
        }

        return new RefreshTokenResponse
        {
            IsSucceed = true,
            Status = 200,
            Error = "",
            AccessToken =
                new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(
                    result, 1)),
            RefreshToken =
                new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(result, 180))
        };
    }

    private async Task<string> IsWrongToken(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var user = await db.Users.FirstOrDefaultAsync(x =>
            x.Id.ToString() == jwtHandler.ReadJwtToken(token).Claims.First().ToString());

        if (user == null)
        {
            return "Invalid token.";
        }

        return user.Id.ToString();
    }
}