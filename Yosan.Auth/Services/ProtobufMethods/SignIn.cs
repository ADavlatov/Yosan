using System.IdentityModel.Tokens.Jwt;
using Yosan.Auth.Contexts;
using Yosan.Auth.Models;
using Yosan.Auth.Services.ValidationMethods;

namespace Yosan.Auth.Services.ProtobufMethods
{
    public class SignIn
    {
        public async Task<SignInResponse> AddUser(SignInRequest request, UserContext db)
        {
            SignInValidator signInValidator = new SignInValidator(db);
            var validationResult = await signInValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new SignInResponse
                {
                    IsSucceed = false,
                    UsernameErrors = string.Join(", ",
                        validationResult.Errors.Where(x => x.ErrorMessage.Contains("username"))),
                    EmailErrors = string.Join(", ", validationResult.Errors.Where(x => x.ErrorMessage.Contains("email"))),
                    PasswordErrors = string.Join(", ",
                        validationResult.Errors.Where(x => x.ErrorMessage.Contains("password")))
                };
            }

            db.Users!.Add(new User
            {
                Username = request.Username, Email = request.Email, Password = request.Password
            });
            db.SaveChanges();

            return new SignInResponse
            {
                IsSucceed = true,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(request.Username, 1)),
                RefreshToken =
                    new JwtSecurityTokenHandler().WriteToken(TokenService.GetJwtToken(request.Username, 15)),
                UserId = db.Users.FirstOrDefault(x =>
                        x.Username == request.Username && x.Email == request.Email && x.Password == request.Password)!.Id
                    .ToString()
            };
        }
    }
}