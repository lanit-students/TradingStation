using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataBaseService.BrokerConsumers
{
    public class GetUserByIdConsumer : IConsumer<InternalGetUserByIdRequest>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdConsumer([FromServices] IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private User GetUserById(InternalGetUserByIdRequest request)
        {
            return userRepository.GetUserById(request.UserId);
        }

        public async Task Consume(ConsumeContext<InternalGetUserByIdRequest> context)
        {
            var response = BrokerResponseWrapper.CreateResponse(GetUserById, context.Message);

            await context.RespondAsync(response);
        }
    }
}
