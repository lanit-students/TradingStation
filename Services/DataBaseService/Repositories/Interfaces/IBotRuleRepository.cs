using DTO;
using DTO.BrokerRequests;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IBotRuleRepository
    {
        void SaveRuleForBot(BotRuleData rule);
    }
}
