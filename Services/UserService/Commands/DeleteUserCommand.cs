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
        private readonly IValidator<DeleteUserRequest> validator;
        
        public DeleteUserCommand([FromServices]IBus busControl, [FromServices] IValidator<DeleteUserRequest> validator)
        {
            this.busControl = busControl;
            this.validator = validator;
        }

        public async Task<bool> Execute(DeleteUserRequest request)
         {
            validator.ValidateAndThrow(request);

            var user = new InternalDeleteUserRequest { UserCredentialsId = request.UserCredentialsId };

            var deleteUserResult = await deleteUser(user);
            if (!deleteUserResult)
            {
                throw new BadRequestException("Unable to delete user.");
            }
            return deleteUserResult;
        }

         
         private async Task<bool> deleteUser(InternalDeleteUserRequest request)
         {
            var uri = new Uri("rabbitmq://localhost/DatabaseService");

            var client = busControl.CreateRequestClient<InternalDeleteUserRequest>(uri).Create(request);

            var response = await client.GetResponse<OperationResult>();

            return response.Message.IsSuccess;
         }
     }
 }
