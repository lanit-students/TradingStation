using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using System;
using System.Collections.Generic;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRepository
    {
        void CreateBot(BotData bot);

        void DeleteBot(Guid ID);

        void EditBot(EditBotRequest request);

        void RunBot(Guid ID);

        void StopBot(Guid ID);

        List<BotData> GetBots(InternalGetBotsRequest request);
    }
}
