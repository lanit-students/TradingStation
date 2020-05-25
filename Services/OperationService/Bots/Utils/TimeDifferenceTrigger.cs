using DTO;
using System;
using System.Collections.Generic;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Interfaces;
using System.Linq;

namespace OperationService.Bots.Utils
{
    public class TimeDifferenceTrigger : Trigger
    {
        private IEnumerable<Candle> candles;
        private int timeMarker;
        private decimal triggerValue;
        private HubConnection connection;
        private Currency currency;

        private DateTime lastTransaction;

        public override event EventHandler<TriggerEventArgs> Triggered;

        public TimeDifferenceTrigger(
            string figi,
            int timeMarker,
            decimal triggerValue,
            string token,
            Currency currency,
            ICommand<GetCandlesRequest, IEnumerable<Candle>> command)
        {
            this.timeMarker = timeMarker;
            this.triggerValue = triggerValue;
            this.currency = currency;

            var request = new GetCandlesRequest()
            {
                Broker = BrokerType.TinkoffBroker,
                Interval = 60,
                Token = token,
                Figi = figi
            };

            candles = command.Execute(request).Result;

            BuildConnection(figi, token, OnMessageReceived).Wait();
        }

        private async Task BuildConnection(string figi, string token, Action<Candle> onReceived)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5009/CandleHub")
                .Build();

            connection.On<Candle>("ReceiveMessage", onReceived.Invoke);

            await connection.StartAsync();

            await connection.SendAsync("Subscribe", new GetCandlesRequest
            {
                Token = token,
                Broker = BrokerType.TinkoffBroker,
                Figi = figi
            });
        }

        private void OnMessageReceived(Candle candle)
        {
            if ((DateTime.Now - lastTransaction).Seconds < 10)
            {
                return;
            }

            if (Check(candle))
            {
                var args = new TriggerEventArgs()
                {
                    Figi = candle.Figi,
                    Price = candle.Close,
                    Currency = currency
                };

                lastTransaction = DateTime.Now;

                Triggered(this, args);
            }
        }

        private bool Check(Candle lastCandle)
        {
            var oldPrice = candles.Where(x => (DateTime.Now - x.Time).TotalMinutes >= timeMarker).FirstOrDefault()?.Close;

            if (oldPrice == null)
            {
                return false;
            }

            var percent = (lastCandle.Close - oldPrice.Value) / lastCandle.Close * 100;

            if (triggerValue >= 0)
            {
                return percent >= triggerValue;
            }

            return percent <= triggerValue;
        }

        public override void Disable()
        {
            connection.StopAsync();
        }
    }
}
