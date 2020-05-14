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
                TimeMarker = dbRule.TimeMarker,
                TriggerValue = dbRule.TriggerValue
            };
        }
    }
}
