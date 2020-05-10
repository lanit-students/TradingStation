using System;
using System.Collections.Generic;

namespace DTO.RestRequests
{
    public class BotInfoResponse
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public bool isRunning { get; set; }
    }
}
