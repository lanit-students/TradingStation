 using System;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using IDeleteUserUserService.Interfaces;
using Kernel.CustomExceptions;
using MassTransit;

namespace UserService.Commands
 {
     public class DeleteUserCommand : IDeleteUserCommand
     {
        private readonly IBus busControl;
        
        public DeleteUserCommand(IBus busControl)
        {
            this.busControl = busControl;
        }

        public async Task<bool> Execute(DeleteUserRequest request)
         {
            var id = request.UserId;

            // Validation for id in common validation or validation for DeleteUserRequest? 

            var user = new InternalDeleteUserRequest { UserId = id };

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
