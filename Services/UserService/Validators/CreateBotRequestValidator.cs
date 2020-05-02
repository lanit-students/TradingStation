using DTO.RestRequests;
using FluentValidation;

namespace UserService.Validators
{
    public class CreateBotRequestValidator : AbstractValidator<CreateBotRequest>
    {
        public CreateBotRequestValidator()
        {
            RuleFor(request => request.bot)
                .NotEmpty()
                .WithMessage(ErrorsMessages.IdIsNullOrEmpty);
        }
    }
}
