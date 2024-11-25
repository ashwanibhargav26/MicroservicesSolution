using AuthServiceApi.Application.Common.Models.User;
using FluentValidation;

namespace AuthServiceApi.Web.Validations;

public class LoginRequestValidation : AbstractValidator<UserSignInRequest>
{
    public LoginRequestValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
    }
}
