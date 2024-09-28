using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Auth;

public class LogInValidator : AbstractValidator<LogInRequest>
{
    public LogInValidator()
    {
        RuleFor(x => x.Username).MinimumLength(4)
            .WithMessage("The username or email must consist of at least 4 characters").MaximumLength(30).NotEmpty()
            .WithMessage("Enter your username or email");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Enter the password");
    }
}