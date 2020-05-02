using DTO.Bots;

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
