using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Kernel;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using FluentValidation;
using System.Threading.Tasks;
using UserService.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using UserService.Utils;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IRequestClient<InternalCreateUserRequest> client;
        private readonly IValidator<CreateUserRequest> validator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CreateUserCommand([FromServices] IRequestClient<InternalCreateUserRequest> client, [FromServices] IValidator<CreateUserRequest> validator,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.client = client;
            this.validator = validator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private async Task<bool> CreateUser(InternalCreateUserRequest request)
        {
            var response = await client.GetResponse<OperationResult<bool>>(request);

            if(OperationResultHandler.HandleResponse(response.Message))
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(request.User);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Account",
                    new { userId = request.User.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(request.User.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

               //return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Execute(CreateUserRequest request)
        {
            validator.ValidateAndThrow(request);

            string passwordHash = ShaHash.GetPasswordHash(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Birthday = request.Birthday,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            UserAvatar userAvatar = null;

            if (request.Avatar != null && request.AvatarExtension != null)
            {
                userAvatar = new UserAvatar
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Avatar = request.Avatar,
                    AvatarExtension = request.AvatarExtension
                };
            }

            var credential = new UserCredential
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            var internalCreateUserRequest = new InternalCreateUserRequest
            {
                User = user,
                Credential = credential,
                UserAvatar = userAvatar
            };

            var createUserResult = await CreateUser(internalCreateUserRequest);

            if (!createUserResult)
            {
                throw new BadRequestException("Unable to create user");
            }

            return createUserResult;
        }
    }
}