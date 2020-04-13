using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
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

        private InternalGetUserByIdResponse GetUserById(InternalGetUserByIdRequest request)
        {
            return userRepository.GetUserWithAvatarById(request.UserId);
        }

        public async Task Consume(ConsumeContext<InternalGetUserByIdRequest> context)
        {
            await context.RespondAsync(GetUserById(context.Message));
        }
    }
}
