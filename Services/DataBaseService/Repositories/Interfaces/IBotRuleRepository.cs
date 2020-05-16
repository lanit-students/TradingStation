using DTO;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRuleRepository
    {
        void SaveRuleForBot(BotRuleData rule);
    }
}
