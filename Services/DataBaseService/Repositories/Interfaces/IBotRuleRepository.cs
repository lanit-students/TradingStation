using DTO;
using System;
using System.Collections.Generic;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRuleRepository
    {
        void SaveRuleForBot(BotRuleData rule);

        List<BotRuleData> GetBotRules(Guid botId);
    }
}
