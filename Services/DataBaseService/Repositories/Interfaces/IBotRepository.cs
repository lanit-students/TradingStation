using DTO;
using DTO.BrokerRequests;
using System;
using System.Collections.Generic;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRepository
    {
        void CreateBot(BotData bot);

        void DeleteBot(Guid ID);

        void RunBot(Guid ID);

        void StopBot(Guid ID);

        List<BotData> GetBots(InternalGetBotsRequest request);
    }
}
