using System;

namespace DTO.Bots
{
    public class Bot : IBot
    {
        public Guid ID;
        public Guid UserID;

        void Run() { }

        void Disable() { }

        void GetStatus() { }

    }
}
