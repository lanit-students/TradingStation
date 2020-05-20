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
            List<BotRuleData> botRulesData, List<string> figis,
            ICommand<TradeRequest, bool> tradeCommand,
            ICommand<GetCandlesRequest, IEnumerable<Candle>> candlesCommand)
        {
            var botRules = new List<BotRule>();

            var botId = botRulesData.First().Id;

            foreach (var data in botRulesData)
            {
                botRules.Add(
                    new BotRule(
                            new StartBotRuleRequest()
                            {
                                UserId = Guid.Empty,
                                BotId = botId,
                                Token = "",
                                Broker = BrokerType.TinkoffBroker,
                                TimeMarker = data.TimeMarker,
                                TriggerValue = data.TriggerValue,
                                OperationType = (OperationType)Enum.GetValues(typeof(OperationType)).GetValue(data.OperationType),
                                MoneyLimitPercents = data.MoneyLimitPercents
                            },
                            tradeCommand,
                            candlesCommand
                        )
                    );
            }

            rules[botId] = botRules;

            foreach (var rule in rules[botId])
            {
                rule.Start(figis);
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
        }
    }
}
