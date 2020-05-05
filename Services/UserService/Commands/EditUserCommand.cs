using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
                logger.LogInformation("Success result from database service received");
            else
                logger.LogWarning("Error from database service received");

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

            try
            {
                await EditUser(internalEditUserInfoRequest);
                return true;
            }
            catch (ForbiddenException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new ForbiddenException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }
            catch (BadRequestException e)
            {
                var errorData = ErrorMessageFormatter.GetMessageData(e.Message);

                var ex = new BadRequestException(errorData.Item3);
                logger.LogWarning(ex, $"{Guid.NewGuid()}_{errorData.Item1}_{errorData.Item3}");
                throw ex;
            }
        }
    }
}