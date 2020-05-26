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
                var link = new LinkBotWithRule
                {
                    Id = Guid.NewGuid(),
                    BotId = rule.BotId,
                    RuleId = rule.Id
                };

                dbContext.LinkBotsWithRules.Add(mapper.MapToDbLink(link));
                dbContext.BotRules.Add(mapper.MapToDbRule(rule));
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Sometheing went wrong during save rule {rule.Id}");
                throw new InternalServerException($"Sometheing went wrong during saving rule {rule.Id}", e);
            }
        }

        public void EditRuleForBot(BotRuleData rule, Guid BotId)
        {
            try
            {
                var link = dbContext.LinkBotsWithRules.FirstOrDefault(x => x.BotId == BotId);
                var rules = dbContext.BotRules.FirstOrDefault(x => x.Id == link.RuleId);

                rules.MoneyLimitPercents = rule.MoneyLimitPercents;
                rules.OperationType=(int)rule.OperationType;
                rules.TimeMarker = rule.TimeMarker;
                rules.TriggerValue = rule.TriggerValue;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var e = new NotFoundException("Not found rule to edit");
                logger.LogWarning(ex, $"{e.Message}, ruleId: {rule.Id}, botId:{rule.BotId}");
                throw e;
            }

        }

        public void DeleteRulesForBot(DeleteBotRequest request)
        {
            try
            {
                var links = dbContext.LinkBotsWithRules.Where(x => x.BotId == request.ID);
                var rules = dbContext.BotRules.Where(x => links.Select(l => l.RuleId).Contains(x.Id));

                foreach (var link in links)
                {
                    dbContext.LinkBotsWithRules.Remove(link);
                }

                dbContext.SaveChanges();

                foreach (var rule in rules)
                {
                    dbContext.BotRules.Remove(rule);
                }

                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new BadRequestException();
            }
        }

        public List<BotRuleData> GetBotRules(Guid botId)
        {
            if (!dbContext.Bots.Select(x => x.Id).Contains(botId))
            {
                throw new NotFoundException();
            }

            var ruleIds = dbContext.LinkBotsWithRules.Where(x => x.BotId == botId).Select(x => x.RuleId).ToList();

            return dbContext.BotRules.Where(r => ruleIds.Contains(r.Id)).Select(r => mapper.MapToRule(r, botId)).ToList();
        }
    }
}
