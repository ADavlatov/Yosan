
using System.IdentityModel.Tokens.Jwt;
using Yosan.Auth.Contexts;
using Yosan.Auth.Services.ValidationMethods;

namespace Yosan.Auth.Services.ProtobufMethods
{
    public class LogIn
    {
        public async Task<LogInResponse> Authorize(LogInRequest request, UserContext db)
        {
            LogInValidator logInValidator = new LogInValidator(db);
            var validationResult = await logInValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new LogInResponse
                {
                    IsSucceed = false,
                    UsernameErrors = string.Join(",",
                        validationResult.Errors.Where(x => x.ErrorMessage.Contains("username"))),
                    PasswordErrors = string.Join(",",
                        validationResult.Errors.Where(x => x.ErrorMessage.Contains("password")))
                };
            }

            return new LogInResponse
            {
                IsSucceed = true,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(request.Username, 1)),
                RefreshToken = new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(request.Username, 4320)),
                UserId = db.Users.FirstOrDefault(x =>
                        (x.Username == request.Username || x.Email == request.Username) && x.Password == request.Password)!
                    .Id
                    .ToString()
            };
        }
    }
}