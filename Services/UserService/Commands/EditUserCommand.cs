using DTO.RestRequests;
using FluentValidation;
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
        
        public EditUserCommand
            ([FromServices] IValidator<UserInfoRequest> userInfoValidator, 
            [FromServices] IValidator<PasswordChangeRequest> passwordChangeValidator)
        {
            this.userInfoValidator = userInfoValidator;
            this.passwordChangeValidator = passwordChangeValidator;
        }
        
        public Task<bool> Execute(EditUserRequest request)
        {
            var passwordRequest = request.PasswordRequest;
            var userInfo = request.UserInfo;
            
            if (!(string.IsNullOrEmpty(passwordRequest.OldPassword)
                    || string.IsNullOrEmpty(passwordRequest.NewPassword)))
            {
                passwordChangeValidator.ValidateAndThrow(passwordRequest);
            }
            userInfoValidator.ValidateAndThrow(userInfo);

            // TODO Realise edit user logic
            throw new NotImplementedException();
        }
    }
}
