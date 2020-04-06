using DTO;
using FluentValidation;

namespace AuthenticationService.Validators
{
    public class UserEmailPasswordValidator : AbstractValidator<UserCredential>
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
                .WithMessage("Password hash must not be empty.")
                .Length(40)
                .WithMessage("Password's hash length must be 40 symbols.");
        }
    }
}
