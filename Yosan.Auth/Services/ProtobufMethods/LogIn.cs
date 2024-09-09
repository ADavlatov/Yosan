
using Yosan.Auth.Contexts;

namespace Yosan.Auth.Services.ProtobufMethods
{
    public class LogIn
    {
        public async Task<LogInResponse> Authorize(LogInRequest request, UserContext db)
        {
            var accessToken = new TokenService().GetJwtToken(request.Username, 60000);
            var refreshToken = new TokenService().GetJwtToken(request.Username, 600000);
            
            return new LogInResponse{ };
        }
    }
}