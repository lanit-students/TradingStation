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
        private readonly IRequestClient<InternalDeleteUserRequest> client;
        private readonly IValidator<DeleteUserRequest> validator;

        public DeleteUserCommand([FromServices]IRequestClient<InternalDeleteUserRequest> client, [FromServices] IValidator<DeleteUserRequest> validator)
        {
            this.client = client;
            this.validator = validator;
        }

        private async Task<bool> DeleteUser(InternalDeleteUserRequest request)
        {
            var result = await client.GetResponse<OperationResult>(request);

            return result.Message.IsSuccess;
        }

        public async Task<bool> Execute(DeleteUserRequest request)
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
     }
 }
