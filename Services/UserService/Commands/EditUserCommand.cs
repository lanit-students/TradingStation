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
        private readonly IValidator<EditUserRequest> userInfoValidator;
        private readonly IValidator<EditUserRequest> userPasswordValidator;
        
        public EditUserCommand
            ([FromServices] IValidator<EditUserRequest> userInfoValidator, 
            [FromServices] IValidator<EditUserRequest> userPasswordValidator)
        {
            this.userInfoValidator = userInfoValidator;
            this.userPasswordValidator = userPasswordValidator;
        }
        
        public Task<bool> Execute(EditUserRequest request)
        {
            if (!(string.IsNullOrEmpty(request.OldPassword)
                    || string.IsNullOrEmpty(request.NewPassword)))
            {
                userPasswordValidator.ValidateAndThrow(request);
            }
            userInfoValidator.ValidateAndThrow(request);

            // TODO Realise edit user logic
            throw new NotImplementedException();
        }
    }
}
