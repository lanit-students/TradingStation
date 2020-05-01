using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class CreateBotRequestValidator : AbstractValidator<CreateBotRequest>
    {
        public CreateBotRequestValidator()
        {
            RuleFor(request => request.userId)
                .NotEmpty()
                .WithMessage(ErrorsMessages.IdIsNullOrEmpty);
        }
    }
}
