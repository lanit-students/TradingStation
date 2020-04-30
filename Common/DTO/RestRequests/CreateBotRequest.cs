using DTO.Bots;
using System;

namespace DTO.RestRequests
{
    public class CreateBotRequest
    {
        public Bot bot;
        public Guid userId;

        public CreateBotRequest(Bot bot, Guid userId)
        {
            this.bot = bot;
            this.userId = userId;
        }
    }
}
