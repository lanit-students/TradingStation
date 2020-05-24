using DataBaseService.Database.Models;
using DTO;
using System;

namespace DataBaseService.Mappers.Interfaces
{
    public interface IBotRuleMapper
    {
        DbBotRule MapToDbRule(BotRuleData rule);

        BotRuleData MapToRule(DbBotRule dbRule, Guid botId);

        DbLinkBotsWithRules MapToDbLink(LinkBotWithRule link);

        LinkBotWithRule MapToLink(DbLinkBotsWithRules dbLink);
    }
}
