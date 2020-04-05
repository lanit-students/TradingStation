using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class EditUserPasswordRequestValidator: AbstractValidator<EditUserRequest>
    {
        public EditUserPasswordRequestValidator()
        {
            RuleFor(user => user.OldPassword)
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmptyPassword);
            RuleFor(user => user.NewPassword)
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmptyPassword);
        }
    }
}
