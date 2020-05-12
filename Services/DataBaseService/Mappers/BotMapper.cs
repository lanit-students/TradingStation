using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DTO;

namespace DataBaseService.Mappers
{
    public class BotMapper : IBotMapper
    {
        public DbBot BotDataToDbBot(BotData data)
        {
            return new DbBot()
            {
                Id = data.Id,
                UserId = data.UserId,
                Name = data.Name,
                IsRunning = data.IsRunning
            };
        }

        public BotData DbBotToBotData(DbBot dbBot)
        {
            return new BotData()
            {
                Id = dbBot.Id,
                UserId = dbBot.UserId,
                Name = dbBot.Name,
                IsRunning = dbBot.IsRunning
            };
        }
    }
}
