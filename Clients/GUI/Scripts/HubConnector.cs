using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Kernel;
using Kernel.Enums;
using Microsoft.AspNetCore.SignalR.Client;

namespace GUI.Scripts
{
    public static class HubConnector
    {
        public static async Task<IEnumerable<Candle>> SubscribeOnCandle(Action<Candle> OnReceivedAction, BrokerType broker, string figi, string token)
        {
            const string url = "https://localhost:5009/operations/candles/get";

            var queryParams = new Dictionary<string, string>
            {
                { "broker", broker.ToString() },
                { "token", token },
                { "figi", figi }
            };

            var client = new RestClient<object, IEnumerable<Candle>>(url, RestRequestType.GET, queryParams: queryParams);

            var listCandles = await client.ExecuteAsync();

            var subscribeOnCandle = listCandles.ToList();

            if (subscribeOnCandle.Count == 0)
            {
                return subscribeOnCandle;
            }

            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5009/CandleHub")
                .Build();

            hubConnection.On<Candle>("ReceiveMessage", OnReceivedAction.Invoke);

            await hubConnection.StartAsync();

            await hubConnection.SendAsync("Subscribe", new GetCandlesRequest
            {
                Token = token,
                Broker = BrokerType.TinkoffBroker,
                Figi = figi
            });

            return subscribeOnCandle;
        }
    }
}
