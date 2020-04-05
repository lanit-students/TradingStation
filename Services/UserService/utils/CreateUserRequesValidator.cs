using DTO.RestRequests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Utils
{
    public class CreateUserRequesValidator: AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequesValidator()
        {
            RuleFor(p => p.Email)
                .Must(isEmail)
                .WithMessage("Incorrect E-mail");

            RuleFor(p => p.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("First name is empty")
                .Must(isName)
                .WithMessage("name contains invalid characters")
                .Length(2,40)
                .WithMessage("Incorrect first name");

            RuleFor(p => p.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("Last name is empty")
                .Must(isName)
                .WithMessage("name contains invalid characters")
                .Length(2, 40)
                .WithMessage("Incorrect length");

            RuleFor(p => p.Birthday)
                .Must(isValidDate)
                .WithMessage("Incorrect birthday");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Password is empty");
        }

        private bool isEmail(string test)
        {
            return new EmailAddressAttribute().IsValid(test);
        }

        private bool isName(string name)
        {
            name = name.Replace("-", "");
            return name.All(char.IsLetter);
        }

        private bool isValidDate(DateTime date)
        {
            if (DateTime.Now.Year - date.Year > 120)
                return false;

            if (DateTime.Now.Year < date.Year)
                return false;

            if (DateTime.Now.Year == date.Year && DateTime.Now.Month < date.Month)
                return false;

            if (DateTime.Now.Year == date.Year && DateTime.Now.Month == date.Month && DateTime.Now.Day < date.Day)
                return false;

            return true;
        }
    }
}
