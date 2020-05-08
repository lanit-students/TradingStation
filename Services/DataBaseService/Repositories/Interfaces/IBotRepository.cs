using DTO;
using System;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRepository
    {
        void CreateBot(BotData bot);

        void DeleteBot(Guid ID);

        void RunBot(Guid ID);

        void StopBot(Guid ID);
    }
}
