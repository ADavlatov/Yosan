using FluentValidation;
using FluentValidation.Validators;
using Yosan.Auth.Contexts;

namespace Yosan.Auth.Services.ValidationMethods;

public class SignInValidator : AbstractValidator<SignInRequest>
{
    public SignInValidator(UserContext db)
    {
        RuleFor(x => x.Username)
            .Must(x => !db.Users.Any(y => y.Username == x))
            .WithMessage("This username already exists").MinimumLength(4)
            .WithMessage("The username must consist of at least 4 characters").MaximumLength(16)
            .WithMessage("The username must not exceed 16 characters in length").NotEmpty()
            .WithMessage("Enter the username");
        RuleFor(x => x.Password).Must(x => x.All(char.IsLetter) || x.All(char.IsNumber))
            .WithMessage("The password must consist only of letters and numbers").MinimumLength(5)
            .WithMessage("The password must consist of at least 5 characters").MaximumLength(30)
            .WithMessage("The password must not exceed 16 characters in length").NotEmpty()
            .WithMessage("Enter the password");
        RuleFor(x => x.Email).Must(x => !db.Users.Any(y => y.Email == x))
            .WithMessage("A user with this email address already exists").NotEmpty()
            .EmailAddress(mode: EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Error when entering email");
    }
}