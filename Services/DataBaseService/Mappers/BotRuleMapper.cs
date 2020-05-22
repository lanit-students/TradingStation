using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;
using DTO.MarketBrokerObjects;
using System;

namespace DataBaseService.Mappers
{
    public class BotRuleMapper : IBotRuleMapper
    {
        public DbBotRule MapToDbRule(BotRuleData rule)
        {
            return new DbBotRule
            {
                Id = rule.Id,
                OperationType = (int)rule.OperationType,
                MoneyLimitPercents = rule.MoneyLimitPercents,
                TimeMarker = rule.TimeMarker,
                // TODO: make it decimal in db
                TriggerValue = (int)rule.TriggerValue
            };
        }

        public BotRuleData MapToRule(DbBotRule dbRule)
        {
            return new BotRuleData
            {
                Id = dbRule.Id,
                OperationType = (OperationType)dbRule.OperationType,
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
