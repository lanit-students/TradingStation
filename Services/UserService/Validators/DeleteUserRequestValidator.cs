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
                .WithMessage("Id must be not null")
                .NotEmpty()
                .WithMessage("Id must be not empty");
        }
    }
}
