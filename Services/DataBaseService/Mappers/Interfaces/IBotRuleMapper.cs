using DataBaseService.Database.Models;
using DTO;

namespace DataBaseService.Mappers.Interfaces
{
    public interface IBotRuleMapper
    {
        DbBotRule MapToDbRule(BotRuleData rule);

        BotRuleData MapToRule(DbBotRule dbRule);
    }
}
