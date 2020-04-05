using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class DeleteUserConsumer : IConsumer<InternalGetUserByIdRequest>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private OperationResult DeleteUser(InternalGetUserByIdRequest request)
        {
            userRepository.DeleteUser(request.UserId);

            return new OperationResult
            {
                IsSuccess = true
            };
        }

        public async Task Consume(ConsumeContext<InternalGetUserByIdRequest> context)
        {
            var deleteResult = DeleteUser(context.Message);

            await context.RespondAsync(deleteResult);
        }
    }
}
