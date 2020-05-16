using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;

namespace DataBaseService.Mappers
{
    public class BotRuleMapper : IBotRuleMapper
    {
        public DbBotRule MapToDbRule(BotRuleData rule)
        {
            return new DbBotRule
            {
                Id = rule.Id,
                OperationType = rule.OperationType,
                MoneyLimitPercents = rule.MoneyLimitPercents,
                TimeMarker = rule.TimeMarker,
                TriggerValue = rule.TriggerValue
            };
        }

        public BotRuleData MapToRule(DbBotRule dbRule)
        {
            return new BotRuleData
            {
                Id = dbRule.Id,
                OperationType = dbRule.OperationType,
                MoneyLimitPercents = dbRule.MoneyLimitPercents,
                TimeMarker = dbRule.TimeMarker,
                TriggerValue = dbRule.TriggerValue
            };
        }

        public DbLinkBotsWithRules MapToDbLink(LinkBotWithRule link)
        {
            return new DbLinkBotsWithRules
            {
                Id = link.Id,
                BotId = link.BotId,
                RuleId = link.RuleId
            };
        }

        public LinkBotWithRule MapToLink(DbLinkBotsWithRules dbLink)
        {
            return new LinkBotWithRule
            {
                Id = dbLink.Id,
                BotId = dbLink.BotId,
                RuleId = dbLink.RuleId
            };
        }

    }
}
