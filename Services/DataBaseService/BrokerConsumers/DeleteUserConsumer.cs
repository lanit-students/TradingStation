using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
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
            userRepository.DeleteUser(request.UserCredentialsId);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalDeleteUserRequest> context)
        {
            var deleteResult = DeleteUser(context.Message);

            await context.RespondAsync(deleteResult);
        }
    }
}
