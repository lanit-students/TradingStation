using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class EditUserCommand : IEditUserCommand
    {
        private readonly IRequestClient<InternalEditUserInfoRequest> client;
        private readonly IValidator<UserInfoRequest> userInfoValidator;
        private readonly IValidator<PasswordChangeRequest> passwordChangeValidator;

        public EditUserCommand
            ([FromServices] IValidator<UserInfoRequest> userInfoValidator,
            [FromServices] IValidator<PasswordChangeRequest> passwordChangeValidator,
            [FromServices] IRequestClient<InternalEditUserInfoRequest> client)
        {
            this.client = client;
            this.userInfoValidator = userInfoValidator;
            this.passwordChangeValidator = passwordChangeValidator;
        }

        private async Task<bool> EditUser(InternalEditUserInfoRequest request)
        {
            var result = await client.GetResponse<OperationResult>(request);

            return result.Message.IsSuccess;
        }

        public async Task<bool> Execute(EditUserRequest request)
        {
            var passwordRequest = request.PasswordRequest;
            var userInfo = request.UserInfo;
            PasswordHashChangeRequest passwordHashChangeRequest = null;

            if (passwordRequest != null)
            {
                passwordChangeValidator.ValidateAndThrow(passwordRequest);
                passwordHashChangeRequest = new PasswordHashChangeRequest
                {
                    OldPasswordHash = ShaHash.GetPasswordHash(passwordRequest.OldPassword),
                    NewPasswordHash = ShaHash.GetPasswordHash(passwordRequest.NewPassword)
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
                UserPasswords = passwordHashChangeRequest
            };

            var editUserResult = await EditUser(internalEditUserInfoRequest);
            if (!editUserResult)
            {
                throw new BadRequestException("Unable to edit");
            }

            return editUserResult;
        }
    }
}