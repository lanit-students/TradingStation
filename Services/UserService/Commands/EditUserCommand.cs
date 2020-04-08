using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class EditUserCommand : IEditUserCommand
    {
        private readonly IValidator<UserInfoRequest> userInfoValidator;
        private readonly IValidator<PasswordChangeRequest> passwordChangeValidator;
        private readonly IBus busControl;

        public EditUserCommand
            ([FromServices] IValidator<UserInfoRequest> userInfoValidator,
            [FromServices] IValidator<PasswordChangeRequest> passwordChangeValidator,
            [FromServices] IBus busControl)
        {
            this.busControl = busControl;
            this.userInfoValidator = userInfoValidator;
            this.passwordChangeValidator = passwordChangeValidator;
        }

        public async Task<bool> Execute(EditUserRequest request)
        {
            var passwordRequest = request.PasswordRequest;
            var userInfo = request.UserInfo;
            PasswordHashChangeRequest passwordHashChangeRequest = null;

            if (!(string.IsNullOrEmpty(passwordRequest.OldPassword)
                    || string.IsNullOrEmpty(passwordRequest.NewPassword)))
            {
                passwordChangeValidator.ValidateAndThrow(passwordRequest);
                passwordHashChangeRequest = new PasswordHashChangeRequest
                {
                    OldPasswordHash = ShaHash.GetPasswordHash(passwordRequest.NewPassword),
                    NewPasswordHash = ShaHash.GetPasswordHash(passwordRequest.OldPassword)
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

        private async Task<bool> EditUser(InternalEditUserInfoRequest request)
        {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<InternalEditUserInfoRequest>(uri).Create(request);

            var response = await client.GetResponse<OperationResult>();

            return response.Message.IsSuccess;
        }
    }
}