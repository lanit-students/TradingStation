using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class EditUserPasswordValidator: AbstractValidator<EditUserRequest>
    {
        public EditUserPasswordValidator()
        {
            RuleFor(user => user.OldPassword)
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmptyPassword);
            RuleFor(user => user.NewPassword)
                .NotEmpty()
                .WithMessage(ErrorsMessages.EmptyPassword);
            RuleFor(user => user.OldPassword)
                 .NotEqual(user => user.NewPassword)
                 .WithMessage(ErrorsMessages.OldAndNewPasswordsEqual);
        }
    }
}
