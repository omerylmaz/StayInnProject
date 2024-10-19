using FluentValidation;

namespace Auth.API.Auth.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FullName).MinimumLength(5);

            RuleFor(x => x.Email).MinimumLength(5);

            RuleFor(x => x.Password).MinimumLength(5);
        }
    }
}
