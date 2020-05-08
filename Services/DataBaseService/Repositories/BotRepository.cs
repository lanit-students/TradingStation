using DataBaseService.Database;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DataBaseService.Repositories
{
    public class BotRepository : IBotRepository
    {
        private readonly TPlatformDbContext dbContext;
        private readonly IBotMapper mapper;
        private readonly ILogger<BotRepository> logger;

        public BotRepository(IBotMapper mapper, TPlatformDbContext dbContext, [FromServices] ILogger<BotRepository> logger)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void CreateBot(BotData bot)
        {
            dbContext.Bots.Add(mapper.BotDataToDbBot(bot));
            dbContext.SaveChanges();
        }

        public void DeleteBot(Guid Id)
        {
            dbContext.Bots.Remove(dbContext.Bots.FirstOrDefault(bot => bot.Id == Id));
            // TODO: remove records from bot rules table also (task for one who implements rules)
        }

        public void RunBot(Guid Id)
        {

        }

        public void StopBot(Guid Id)
        {
            try
            {
                dbContext.Bots.FirstOrDefault(bot => bot.Id == Id).IsRunning = false;
            }
            catch
            {
                throw new NotFoundException();
            }
        }
    }
}
