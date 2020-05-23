using DataBaseService.Database.Models;
using DTO;

namespace DataBaseService.Mappers.Interfaces
{
    public interface IBotMapper
    {
        DbBot BotDataToDbBot(BotData data);

        BotData DbBotToBotData(DbBot dbBot);
    }
}
