using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class EditUserCommand : IEditUserCommand
    {
        private readonly IRequestClient<InternalEditUserInfoRequest> client;
        private readonly IValidator<UserInfoRequest> userInfoValidator;
        private readonly IValidator<PasswordChangeRequest> passwordChangeValidator;
        private readonly IValidator<AvatarChangeRequest> avatarValidator;
        private readonly ILogger<EditUserCommand> logger;

        public EditUserCommand
            ([FromServices] IValidator<UserInfoRequest> userInfoValidator,
            [FromServices] IValidator<PasswordChangeRequest> passwordChangeValidator,
            [FromServices] IValidator<AvatarChangeRequest> avatarValidator,
            [FromServices] IRequestClient<InternalEditUserInfoRequest> client,
            [FromServices] ILogger<EditUserCommand> logger)
        {
            this.client = client;
            this.userInfoValidator = userInfoValidator;
            this.passwordChangeValidator = passwordChangeValidator;
            this.avatarValidator = avatarValidator;
            this.logger = logger;
        }

        private async Task<bool> EditUser(InternalEditUserInfoRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);
            if (response.Message.IsSuccess)
                logger.LogInformation("User was edited successfully");
            else
                logger.LogWarning("Error in user edition");

            return OperationResultHandler.HandleResponse(response.Message);
        }

        public async Task<bool> Execute(EditUserRequest request)
        {
            var passwordRequest = request.PasswordRequest;
            var userInfo = request.UserInfo;
            var avatarRequest = request.AvatarRequest;
            PasswordHashChangeRequest passwordHashChangeRequest = null;
            UserAvatar userAvatar = null;

            if (passwordRequest != null)
            {
                passwordChangeValidator.ValidateAndThrow(passwordRequest);
                passwordHashChangeRequest = new PasswordHashChangeRequest
                {
                    OldPasswordHash = ShaHash.GetPasswordHash(passwordRequest.OldPassword),
                    NewPasswordHash = ShaHash.GetPasswordHash(passwordRequest.NewPassword)
                };
            }
            if (avatarRequest != null)
            {
                avatarValidator.ValidateAndThrow(avatarRequest);
                userAvatar = new UserAvatar
                {
                    Avatar = avatarRequest.Avatar,
                    AvatarExtension = avatarRequest.AvatarExtension
                };
            }

            userInfoValidator.ValidateAndThrow(userInfo);

            var user = new User
            {
                Id = userInfo.UserId,
                Birthday = userInfo.Birthday,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Email = userInfo.Email
            };

            var internalEditUserInfoRequest = new InternalEditUserInfoRequest
            {
                User = user,
                UserPasswords = passwordHashChangeRequest,
                UserAvatar = userAvatar                
            };

            var editUserResult = await EditUser(internalEditUserInfoRequest);
            if (!editUserResult)
            {
                var exception = new BadRequestException("Unable to edit");
                logger.LogWarning(exception, "Bad request exception was catched on edit user command");
                throw exception;
            }

            return true;
        }
    }
}