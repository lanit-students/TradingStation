using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MassTransit;

using AuthenticationService.Interfaces;
using DTO;

namespace AuthenticationService.BrokerConsumers
{
    public class TokenConsumer : IConsumer<UserToken>
    {
        private readonly ITokensEngine _tokensEngine;

        public TokenConsumer([FromServices] ITokensEngine tokensEngine)
        {
            _tokensEngine = tokensEngine;
        }

        public async Task Consume(ConsumeContext<UserToken> context)
        {
            await context.RespondAsync(_tokensEngine.CheckToken(context.Message));
        }
    }
}
