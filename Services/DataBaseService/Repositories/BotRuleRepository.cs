using DataBaseService.Database;
using DataBaseService.Mappers.Interfaces;
using DataBaseService.Repositories.Interfaces;
using DTO;
using DTO.BrokerRequests;
using Kernel.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Repositories
{
    public class BotRuleRepository : IBotRuleRepository
    {
        private readonly IBotRuleMapper mapper;
        private readonly TPlatformDbContext dbContext;
        private readonly ILogger<BotRuleRepository> logger;

        public BotRuleRepository(
            IBotRuleMapper mapper, TPlatformDbContext dbContext,
            [FromServices] ILogger<BotRuleRepository> logger
            )
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public void SaveRuleForBot(BotRuleData rule)
        {
            try
            {
                dbContext.BotRules.Add(mapper.MapToDbRule(rule));
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Sometheing went wrong during save rule {rule.Id}");
                throw new InternalServerException($"Sometheing went wrong during saving rule {rule.Id}", e);
            }
        }
    }
}
