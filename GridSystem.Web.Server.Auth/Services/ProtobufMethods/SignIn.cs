using System.IdentityModel.Tokens.Jwt;
using GridSystem.Web.Server.Auth.Contexts;
using GridSystem.Web.Server.Auth.Models;
using GridSystem.Web.Server.Auth.Services.ValidationMethods;

namespace GridSystem.Web.Server.Auth.Services.ProtobufMethods
{
    public class SignIn
    {
        public async Task<SignInResponse> AddUser(SignInRequest request, UserContext db)
        {
            var errors = await new UserValidator().ValidateSignInRequest(request, db);
            
            if (errors != "")
            {
                return new SignInResponse
                {
                    IsSucceed = false,
                    Error = errors
                };
            }

            var user = new User
            {
                Username = request.Username, Email = request.Email, Password = request.Password
            };
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return new SignInResponse
            {
                IsSucceed = true,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(request.Username, 1)),
                RefreshToken =
                    new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(request.Username, 15)),
                UserId = user.Id.ToString()
            };
        }
    }
}