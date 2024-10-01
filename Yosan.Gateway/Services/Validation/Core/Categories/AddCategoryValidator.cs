using System.IdentityModel.Tokens.Jwt;
using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class AddCategoryValidator : AbstractValidator<AddCategoryRequest>
{
    public AddCategoryValidator()
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        RuleFor(x => x.AccessToken).Must(x => jwtHandler.CanReadToken(x)).WithMessage("Wrong token");
        RuleFor(x => x.Name).Empty().WithMessage("Name can't be empty").MaximumLength(50)
            .WithMessage("Name is too long");
        RuleFor(x => x.Type).Empty().WithMessage("Type can't be empty").Must(x => x == 1 || x == 2)
            .WithMessage("Type can be 1 or 2");
    }
}