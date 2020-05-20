using System;
using System.Collections.Generic;

namespace DTO.RestRequests
{
    public class RunBotRequest
    {
        public Guid Id { get; set; }

        public List<string> Figis { get; set; }
    }
}
