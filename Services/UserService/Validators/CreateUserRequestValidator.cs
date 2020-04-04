using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(user => user.Email)
                .NotNull()
                .WithMessage("Email must be.")
                .NotEmpty()
                .WithMessage("Email address must not be empty.")
                .EmailAddress()
                .WithMessage("Email address must be in valid format.");

            RuleFor(user => user.Password)
                .NotNull()
                .WithMessage("Password must be.")
                .NotEmpty()
                .WithMessage("Password must not be empty.");

            RuleFor(user => user.FirstName)
                .NotNull()
                .WithMessage("First name must be.")
                .NotEmpty()
                .WithMessage("First name must not be empty.")
                .Matches(@"^[a-zA-Z-']$")
                .WithMessage("First name must not contain numbers and etc.")
                .Matches(@"/^[a-z ,.'-]+$/i")
                .WithMessage("The first letter must be uppercase and the rest must be lowercase");

            RuleFor(user => user.LastName)
                .NotNull()
                .WithMessage("Last name must be.")
                .NotEmpty()
                .WithMessage("Last name address must not be empty.")
                .Matches(@"^[a-zA-Z-']$")
                .WithMessage("Last name must not contain numbers and etc.")
                .Matches(@"/^[a-z ,.'-]+$/i")
                .WithMessage("The first letter must be uppercase and the rest must be lowercase");

            RuleFor(user => user.Birthday)
                .NotEmpty()
                .WithMessage("Birthday must not be empty.")
                .Must(birthday => birthday <= DateTime.Today)
                .WithMessage("You cannot be born in the future.");
        }
    }
}
