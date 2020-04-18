using DTO.RestRequests;
using FluentValidation;
using System.Collections.Generic;

namespace UserService.Validators
{
    public class AvatarChangeRequestValidator : AbstractValidator<AvatarChangeRequest>
    {
        private const int CMaxSizeOfImageInBytes = 2097152;
        private List<string> extensionFormats = new List<string>{"jpeg", "jpg", "png"};
        public AvatarChangeRequestValidator()
        {
            RuleFor(user => user.Avatar)
                .NotEmpty().When(user => !string.IsNullOrEmpty(user.AvatarExtension))
                .WithMessage(ErrorsMessages.AvatarNullAvatarExtensionNotNull)
                .Must(avatar => avatar == null || avatar.Length <= CMaxSizeOfImageInBytes)
                .WithMessage(ErrorsMessages.AvatarTooBig);
            RuleFor (user => user.AvatarExtension)
                .NotEmpty().When(user => user.Avatar != null)
                .WithMessage(ErrorsMessages.AvatarExtensionNullAvatarNotNull)
                .Must(extension => extension == null || extensionFormats.Contains(extension))
                .WithMessage(ErrorsMessages.AvatarExtensionNotCorrect);
        }
    }
}
