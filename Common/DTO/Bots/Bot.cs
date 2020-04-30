using System;

namespace DTO.Bots
{
    public class Bot : IBot
    {
        public Guid ID;
        public Guid UserID;

        public void Run(BotRules rules) { }

        public void Disable() { }

        public void GetStatus() { }

    }
}
