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

            RuleFor(user => user.PasswordHash)
                .NotEmpty()
                .WithMessage("Password must not be empty.")
                .Length(5, 50)
                .WithMessage("Password's length must be from 5 to 50 symbols.");
        }
    }
}
