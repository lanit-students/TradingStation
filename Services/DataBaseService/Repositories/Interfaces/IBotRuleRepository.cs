using DataBaseService.Database.Models;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using System;
using System.Collections.Generic;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRuleRepository
    {
        void SaveRuleForBot(BotRuleData rule);

        void EditRuleForBot(BotRuleData rule, Guid BotId);

        void DeleteRulesForBot(DeleteBotRequest request);

        List<BotRuleData> GetBotRules(Guid botId);
    }
}
