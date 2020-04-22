using DataBaseService.Repositories.Interfaces;
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

        private bool DeleteUser(InternalDeleteUserRequest request)
        {
            userRepository.DeleteUser(request.UserId);

            return true;
        }

        public async Task Consume(ConsumeContext<InternalDeleteUserRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(DeleteUser, context.Message);

            await context.RespondAsync(response);
        }
    }
}
