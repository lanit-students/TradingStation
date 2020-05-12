using DataBaseService.Database.Models;
using DTO;

namespace DataBaseService.Mappers.Interfaces
{
    public interface IBotRuleMapper
    {
        DbBotRule MapToDbRule(BotRule rule);

        BotRule MapToRule(DbBotRule dbRule);
    }
}
