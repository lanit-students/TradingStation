using DataBaseService.Database;
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
using System.Data;
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
            try
            {
                dbContext.Bots.Remove(dbContext.Bots.FirstOrDefault(bot => bot.Id == Id));
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var e = new NotFoundException("Not found bot to delete");
                logger.LogWarning(ex, $"{e.Message}, botId: {Id}");
                throw e;
            }
            // TODO: remove records from bot rules table also (task for one who implements rules)
        }

        public void EditBot(EditBotRequest request)
        {
            var dbBot = dbContext.Bots.FirstOrDefault(b => b.Id == request.BotId);
            if (dbBot == null)
            {
                var e = new NotFoundException("Not found bot to edit");
                logger.LogWarning(e, $"{e.Message}, botId: {request.BotId}");
                throw e;
            }

            dbBot.Name = request.Name;
            dbContext.SaveChanges();
        }

        public void RunBot(Guid Id)
        {
            try
            {
                dbContext.Bots.FirstOrDefault(bot => bot.Id == Id).IsRunning = true;
                dbContext.SaveChanges();
            }
            catch
            {
                var e = new NotFoundException("Not found bot to run");
                logger.LogWarning(e, $"{e.Message}, botId: {Id}");
                throw e;
            }
        }

        public void StopBot(Guid Id)
        {
            try
            {
                dbContext.Bots.FirstOrDefault(bot => bot.Id == Id).IsRunning = false;
                dbContext.SaveChanges();
            }
            catch
            {
                var e = new NotFoundException("Not found bot to disable");
                logger.LogWarning(e, $"{e.Message}, botId: {Id}");
                throw e;
            }
        }

        public List<BotData> GetBots(InternalGetBotsRequest request)
        {
            var dbbots = dbContext.Bots.Where(bot => bot.UserId == request.UserId);

            if (dbbots == null) return new List<BotData>();

            var bots = new List<BotData>();

            foreach(var dbbot in dbbots)
            {
                bots.Add(mapper.DbBotToBotData(dbbot));
            }

            return bots;
        }
    }
}
