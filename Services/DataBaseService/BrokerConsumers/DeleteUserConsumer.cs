using MassTransit;
using DTO.BrokerRequests;
using System.Threading.Tasks;
using DataBaseService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DTO;

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
            userRepository.DeleteUser(request.Id);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalDeleteUserRequest> context)
        {
            var creationResult = DeleteUser(context.Message);

            await context.RespondAsync(creationResult);
        }
    }
}
