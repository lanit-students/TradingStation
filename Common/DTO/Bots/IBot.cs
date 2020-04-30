namespace DTO.Bots
{
    interface IBot
    {
        void Run(BotRules rules) { }

        void Disable() { }

        void GetStatus() { }
    }
}
