using System;
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
                .MaximumLength(50)
                .WithMessage(ErrorsMessages.EmailTooLong)
                .EmailAddress()
                .WithMessage(ErrorsMessages.InvalidEmail);

            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmptyPassword);

            RuleFor(user => user.FirstName)
                .NotEmpty()
                .WithMessage(ErrorsMessages.FirstNameEmpty)
                .MaximumLength(32)
                .WithMessage(ErrorsMessages.FirstNameTooLong)
                .MinimumLength(2)
                .WithMessage(ErrorsMessages.FirstNameTooShort)
                .Matches("^[A-Z][a-z]+$")
                .WithMessage(ErrorsMessages.FirstNameError);

            RuleFor(user => user.LastName)
                .NotEmpty()
                .WithMessage(ErrorsMessages.LastNameEmpty)
                .MaximumLength(32)
                .WithMessage(ErrorsMessages.LastNameTooLong)
                .MinimumLength(2)
                .WithMessage(ErrorsMessages.LastNameTooShort)
                .Matches("^[A-Z][a-z]+$")
                .WithMessage(ErrorsMessages.LastNameError);

            RuleFor(user => user.Birthday)
                .NotEmpty()
                .WithMessage(ErrorsMessages.BirthdayEmpty)
                .Must(birthday => birthday.AddYears(18) <= DateTime.Today)
                .WithMessage(ErrorsMessages.BirthdayYoung);

        }
    }
}
