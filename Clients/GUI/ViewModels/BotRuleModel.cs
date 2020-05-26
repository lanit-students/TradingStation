using DTO;
using DTO.MarketBrokerObjects;
using System;

namespace GUI.ViewModels
{
    public class BotRuleModel
    {
        public bool IsChoosen { get; set; }
        public OperationType OperationType { get; set; }
        public int MoneyLimitPercents { get; set; }
        public TimeSpan TimeMarker { get; set; }
        public decimal TriggerValue { get; set; }
        public string Description { get; set; }

        public BotRuleModel(BotRuleData rule)
        {
            OperationType = rule.OperationType;
            MoneyLimitPercents = rule.MoneyLimitPercents;
            TimeMarker = new TimeSpan(0, rule.TimeMarker, 0);
            TriggerValue = rule.TriggerValue;
            IsChoosen = true;
            Description = rule.OperationType == OperationType.Buy ? CBuyDescription : CSellDescription;
        }

        public BotRuleModel(OperationType operation)
        {
            IsChoosen = false;
            OperationType = operation;
            MoneyLimitPercents = 20;
            TimeMarker = new TimeSpan(0, 10, 0);
            TriggerValue = 0.1m;
            Description = operation == OperationType.Buy ? CBuyDescription : CSellDescription;
        }

        private const string CBuyDescription = "This rule involves buying the instrument when the price is lower than the selected value.\n" +
                    "Transactions can be made within the selected percentage of the user's balance\n" +
                    "The bot checks the pricing policies with the selected time interval\n";
        private const string CSellDescription = "This rule involves selling the instrument when the price is higher than the selected value.\n" +
            "Transactions can be made within the selected percentage of the user's balance\n" +
            "The bot checks the pricing policies with the selected time interval\n";
    }
}
