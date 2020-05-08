using System;
using System.Collections.Generic;

namespace DTO.Bots
{
    public class Bot
    {
        public string Name;
        public Guid ID;
        public Guid UserID;
        public List<BotRule> Rules;
        public bool isActive;
        public Status CurrentStatus;

        public void Run() { }

        public void Disable() { }
    }
}
