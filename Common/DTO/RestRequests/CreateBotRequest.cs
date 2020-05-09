using System;

namespace DTO.RestRequests
{
    public class CreateBotRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
