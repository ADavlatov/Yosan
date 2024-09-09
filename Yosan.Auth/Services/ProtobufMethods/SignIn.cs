using Yosan.Auth.Contexts;
using Yosan.Auth.Models;

namespace Yosan.Auth.Services.ProtobufMethods
{
    public class SignIn
    {
        public async Task<SignInResponse> AddUser(SignInRequest request, UserContext db)
        {
            await db.Users.AddAsync(new User
                { Username = request.Username, Email = request.Email, Password = request.Password });
            await db.SaveChangesAsync();

            var tokenService = new TokenService();
            var user = db.Users.FirstOrDefault(x => x.Username == request.Username);

            if (user == null)
            {
                return new SignInResponse { IsSucceed = false, Error = "Ошибка добавления пользователя" };
            }

            return new SignInResponse
            {
                IsSucceed = true, Error = "", StatusCode = 200,
                AccessToken = tokenService.GetJwtToken(request.Username, 60000).ToString(),
                RefreshToken = tokenService.GetJwtToken(request.Username, 600000).ToString(),
                UserId = user.Id.ToString()
            };
        }
    }
}