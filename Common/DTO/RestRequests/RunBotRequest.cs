using System;
using System.Collections.Generic;

namespace DTO.RestRequests
{
    public class RunBotRequest
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public Guid BotId { get; set; }

        public List<string> Figis { get; set; }
    }
}
