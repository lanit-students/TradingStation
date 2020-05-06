using DTO.Bots;
using System;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRepository
    {
        void CreateBot(Bot bot);

        void DeleteBot(Guid ID);

        void Run(Guid ID);

        void Disable(Guid ID);

//        List<Bot> GetBots();
    }
}
