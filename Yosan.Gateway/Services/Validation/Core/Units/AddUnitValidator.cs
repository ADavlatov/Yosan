using System.IdentityModel.Tokens.Jwt;
using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Units;

public class AddUnitValidator : AbstractValidator<AddUnitRequest>
{
    public AddUnitValidator()
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        RuleFor(x => x.AccessToken).Must(x => jwtHandler.CanReadToken(x)).WithMessage("Wrong token");
        RuleFor(x => x.CategoryId).Empty().WithMessage("CategoryId can't be empty");
        RuleFor(x => x.Name).Empty().WithMessage("Name can't be empty").MaximumLength(50)
            .WithMessage("Name is too long");
        RuleFor(x => x.Date).Must(x => DateOnly.TryParse(x, out _)).WithMessage("Wrong date format");
        RuleFor(x => x.Sum).Must(x => float.TryParse(x, out _)).WithMessage("Wrong sum format");
    }
}