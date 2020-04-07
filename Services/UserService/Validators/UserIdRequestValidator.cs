using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class UserIdRequestValidator: AbstractValidator<UserIdRequest>
    {
        public UserIdRequestValidator()
        {
            RuleFor(request => request.UserId)
                .NotEmpty()
                .WithMessage(ErrorsMessages.IdIsNullOrEmpty);
        }
    }
}
