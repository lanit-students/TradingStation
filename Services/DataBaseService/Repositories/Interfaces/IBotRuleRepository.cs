using DTO;
using DTO.RestRequests;
using System;
using System.Collections.Generic;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRuleRepository
    {
        void SaveRuleForBot(BotRuleData rule);

        void DeleteRulesForBot(DeleteBotRequest request);

        List<BotRuleData> GetBotRules(Guid botId);
    }
}
