using DTO;
using FluentValidation;

namespace AuthenticationService.Validators
{
    public class SignInModelValidation : AbstractValidator<UserEmailPassword>
    {
        public SignInModelValidation()
        {
            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage("Email address must be valid")
                .NotEmpty()
                .WithMessage("Email address must not be empty.");

            RuleFor(user => user.Password)
                .Length(5, 500)
                .WithMessage("Password's length must be from 5 to 500 symbols")
                .NotEmpty()
                .WithMessage("Password must not be empty.");
        }
    }
}
