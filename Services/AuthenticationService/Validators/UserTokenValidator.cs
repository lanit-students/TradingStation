using DTO;
using FluentValidation;
using System;

namespace AuthenticationService.Validators
{
    public class UserTokenValidator : AbstractValidator<UserToken>
    {
        public UserTokenValidator()
        {
            RuleFor(token => token.Body)
                .NotEmpty()
                .WithMessage("Token body must not be empty.");

            RuleFor(token => token.UserId)
                .Must(id => id != Guid.Empty)
                .WithMessage("User ID must not be empty.");
        }
    }
}
