using System;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using FluentValidation;
using IDeleteUserUserService.Interfaces;
using Kernel.CustomExceptions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Commands
 {
     public class DeleteUserCommand : IDeleteUserCommand
     {
        private readonly IBus busControl;
        private readonly IValidator<UserIdRequest> validator;

        public DeleteUserCommand([FromServices]IBus busControl, [FromServices] IValidator<UserIdRequest> validator)
        {
            this.busControl = busControl;
            this.validator = validator;
        }

        public async Task<bool> Execute(UserIdRequest request)
         {
            validator.ValidateAndThrow(request);

            var user = new InternalDeleteUserRequest { UserId = request.UserId };

            var deleteUserResult = await DeleteUser(user);
            if (!deleteUserResult)
            {
                throw new BadRequestException("Unable to delete user.");
            }
            return deleteUserResult;
        }


         private async Task<bool> DeleteUser(InternalDeleteUserRequest request)
         {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<InternalDeleteUserRequest>(uri).Create(request);

            var response = await client.GetResponse<OperationResult>();

            return response.Message.IsSuccess;
         }
     }
 }
