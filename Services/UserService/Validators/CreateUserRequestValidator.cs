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
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmailEmpty)
                .EmailAddress()
                .WithMessage(ErrorsMessages.InvalidEmail);

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmptyPassword);

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .WithMessage(ErrorsMessages.FirstNameEmpty)
                .Matches("^[A-Z][a-z]+$")
                .WithMessage(ErrorsMessages.FirstNameError);

            RuleFor(user => user.LastName)
                .NotEmpty()
                .WithMessage(ErrorsMessages.LastNameEmpty)
                .Matches("^[A-Z][a-z]+$")
                .WithMessage(ErrorsMessages.LastNameError);

            RuleFor(user => user.Birthday)
                .NotEmpty()
                .WithMessage(ErrorsMessages.BirthdatEmpty)
                .Must(birthday => birthday <= DateTime.Today)
                .WithMessage(ErrorsMessages.FutureErrorBirthday);
        }
    }
}
