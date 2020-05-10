using DataBaseService.Database;
using DataBaseService.Database.Models;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Kernel.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            try
            {
                dbContext.Bots.FirstOrDefault(bot => bot.Id == Id).IsRunning = true;
            }
            catch
            {
                throw new NotFoundException();
            }
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

        public List<BotData> GetBots(InternalGetBotsRequest request)
        {            
            var dbbots = dbContext.Bots.Where(bot => bot.UserId == request.UserId);

            if (dbbots == null) return new List<BotData>();

            var bots = new List<BotData>();

            foreach(var dbbot in dbbots)
            {
                var bot = new BotData()
                {
                    Id = dbbot.Id,
                    UserId = dbbot.UserId,
                    Name = dbbot.Name,
                    IsRunning = dbbot.IsRunning
                };
                bots.Add(bot);
            }

            return bots;
        }
    }
}
