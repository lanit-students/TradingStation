using System;
using System.Collections.Generic;

namespace DTO.RestRequests
{
    public class BotInfoRequest
    {
        public string Name;
        public List<string> Rules;
        public Guid ID;
    }
}
