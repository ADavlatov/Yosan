using System.IdentityModel.Tokens.Jwt;
using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class GetCategoriesValidator : AbstractValidator<GetCategoriesRequest>
{
    public GetCategoriesValidator()
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        RuleFor(x => x.AccessToken).Must(x => jwtHandler.CanReadToken(x)).WithMessage("Wrong token");    }
}