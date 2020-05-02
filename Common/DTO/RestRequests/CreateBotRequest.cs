using DTO.Bots;
using System;

namespace DTO.RestRequests
{
    public class CreateBotRequest
    {
        public Bot bot;

        public CreateBotRequest(Bot bot)
        {
            this.bot = bot;
        }
    }
}
