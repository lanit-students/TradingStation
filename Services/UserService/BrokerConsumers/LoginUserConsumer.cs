using DTO;
using DTO.BrokerRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UserService.BrokerConsumers
{
    public class LoginUserConsumer : IConsumer<InternalLoginRequest>
    {
        private readonly IRequestClient<InternalLoginRequest> client;

        public LoginUserConsumer([FromServices] IRequestClient<InternalLoginRequest> client)
        {
            this.client = client;
        }

        public async Task Consume(ConsumeContext<InternalLoginRequest> context)
        {
            var response = await client.GetResponse<OperationResult<UserCredential>>(context.Message);

            await context.RespondAsync(response.Message);
        }
    }
}
