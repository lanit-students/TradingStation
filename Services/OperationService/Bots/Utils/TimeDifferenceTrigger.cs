using DTO;
using Microsoft.AspNetCore.Mvc;
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
        private Currency currency;

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

            var request = new GetCandlesRequest()
            {
                Broker = BrokerType.TinkoffBroker,
                Token = token,
                Figi = figi
            };

            candles = command.Execute(request).Result;

            BuildConnection(figi, token, OnMessageReceived).Wait();
        }

        private async Task BuildConnection(string figi, string token, Action<Candle> onReceived)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5009/CandleHub")
                .Build();

            hubConnection.On<Candle>("ReceiveMessage", onReceived.Invoke);

            await hubConnection.StartAsync();

            await hubConnection.SendAsync("Subscribe", new GetCandlesRequest
            {
                Token = token,
                Broker = BrokerType.TinkoffBroker,
                Figi = figi
            });
        }

        private void OnMessageReceived(Candle candle)
        {
            if (Check(candle))
            {
                var args = new TriggerEventArgs()
                {
                    Figi = candle.Figi,
                    Price = candle.Close,
                    // TODO: add correct currency
                    Currency = currency
                };

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

            return (lastCandle.Close - oldPrice.Value) >= triggerValue;
        }
    }
}
