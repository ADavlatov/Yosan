using FluentValidation;
using Yosan.Auth.Contexts;

namespace Yosan.Auth.Services.ValidationMethods;

public class LogInValidator : AbstractValidator<LogInRequest>
{
    public LogInValidator(UserContext db)
    {
        RuleFor(x => x)
            .Must(x => db.Users.FirstOrDefault(y =>
                (y.Username == x.Username || y.Email == x.Username) && y.Password == x.Password) != null)
            .WithMessage("Invalid username or password");
        RuleFor(x => x.Username).MinimumLength(4)
            .WithMessage("The username or email must consist of at least 4 characters").MaximumLength(30).NotEmpty()
            .WithMessage("Enter your username or email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Enter the password");
    }
}