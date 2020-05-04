using System;
using System.Collections.Generic;

namespace DTO.Bots
{
    public class Bot : IBot
    {
        public string Name;
        public Guid ID;
        public Guid UserID;
        public List<BotRules> Rules;
        public void Run() { }

        public void Disable() { }

        public void GetStatus() { }

    }
}
