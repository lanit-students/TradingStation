using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class DeleteUserConsumer : IConsumer<InternalDeleteUserRequest>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private OperationResult DeleteUser(InternalDeleteUserRequest request)
        {
            userRepository.DeleteUser(request.UserId);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalDeleteUserRequest> context)
        {
            var response = BrokerResponseWrapper.CreateResponse(DeleteUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}
