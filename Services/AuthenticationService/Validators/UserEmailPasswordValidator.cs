using DTO;
using FluentValidation;

namespace AuthenticationService.Validators
{
    public class UserEmailPasswordValidator : AbstractValidator<UserEmailPassword>
    {
        public UserEmailPasswordValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email address must not be empty.")
                .EmailAddress()
                .WithMessage("Email address must be in valid format.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password must not be empty.")
                .Length(5, 500)
                .WithMessage("Password's length must be from 5 to 500 symbols.")
                .Matches(@"^[a-zA-Z0-9]+$")
                .WithMessage("Password must be alphanumeric.");
        }
    }
}
