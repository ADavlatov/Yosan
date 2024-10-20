using GridSystem.Web.Server.Auth.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GridSystem.Web.Server.Auth.Services.ValidationMethods;

public class UserValidator
{
    public async Task<string> ValidateLogInRequest(LogInRequest request, UserContext db)
    {
        var user = await db.Users.FirstOrDefaultAsync(
            x => x.Username == request.Username || x.Email == request.Username);

        if (user == null)
        {
            return "User does not exist";
        }

        if (user.Password != request.Password)
        {
            return "Wrong password";
        }

        return "";
    }

    public async Task<string> ValidateSignInRequest(SignInRequest request, UserContext db)
    {
        var username = await db.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
        var email = await db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (username != null)
        {
            return "Username already exist";
        }

        if (email != null)
        {
            return "Email already exist";
        }

        return "";
    }
}