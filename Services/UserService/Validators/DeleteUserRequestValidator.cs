using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class DeleteUserRequestValidator: AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(request => request.UserId)
                .NotNull()
                .WithMessage(ErrorsMessages.IdNull)
                .NotEmpty()
                .WithMessage(ErrorsMessages.IdEmpty);
        }
    }
}
