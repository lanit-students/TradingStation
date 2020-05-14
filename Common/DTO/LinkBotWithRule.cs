using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LinkBotWithRule
    {
        public Guid Id { get; set; }
        public Guid BotId { get; set; }
        public Guid RuleId { get; set; }
    }
}
