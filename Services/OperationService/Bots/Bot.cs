using System;
using System.Collections.Generic;

namespace OperationService.Bots
{
    public class Bot
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool IsRunning { get; set; }

        public List<BotRule> Rules { get; set; }

        public void Run() { }

        public void Stop() { }
    }
}
