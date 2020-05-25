using DTO;
using DTO.BrokerRequests;
using DTO.RestRequests;
using Interfaces;
using Kernel.CustomExceptions;
using OperationService.Bots.BotRules;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO.MarketBrokerObjects;

namespace OperationService.Bots
{
    public static class BotRunner
    {
        private static Dictionary<Guid, List<BotRule>> rules =  new Dictionary<Guid, List<BotRule>>();

        public static void Run(
            RunBotRequest request,
            List<BotRuleData> botRulesData,
            ICommand<TradeRequest, bool> tradeCommand,
            ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand,
            ICommand<GetUserBalanceRequest, UserBalance> balanceCommand,
            ICommand<GetInstrumentsRequest, IEnumerable<Instrument>> instrumentsCommand)
        {
            if (botRulesData.Count == 0)
            {
                throw new BadRequestException("Provide bot with some rules before run.");
            }

            if (request.Figis == null || request.Figis.Count == 0)
            {
                throw new BadRequestException("Provide bot rule with figis before run.");
            }

            var botRules = new List<BotRule>();

            var botId = botRulesData.First().BotId;

            if (rules.ContainsKey(botId))
            {
                throw new BadRequestException("Bot is already running.");
            }

            foreach (var rule in botRulesData)
            {
                botRules.Add(
                    new BotRule(
                            new StartBotRuleRequest()
                            {
                                UserId = request.UserId,
                                BotId = botId,
                                Token = request.Token,
                                Broker = BrokerType.TinkoffBroker,
                                TimeMarker = rule.TimeMarker,
                                TriggerValue = rule.TriggerValue,
                                OperationType = rule.OperationType,
                                MoneyLimitPercents = rule.MoneyLimitPercents
                            },
                            tradeCommand,
                            candlesCommand,
                            balanceCommand,
                            instrumentsCommand
                        )
                    );
            }

            rules[botId] = botRules;

            foreach (var rule in rules[botId])
            {
                rule.Start(request.Figis);
            }
        }

        public static void Stop(Guid botId)
        {
            if (!rules.ContainsKey(botId))
            {
                throw new NotFoundException();
            }

            foreach (var rule in rules[botId])
            {
                rule.Stop();
            }

            rules.Remove(botId);
        }

        public static bool IsRunning(Guid botId)
        {
            return rules.ContainsKey(botId);
        }
    }
}
