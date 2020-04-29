using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using ChartJs.Blazor.ChartJS.Common.Time;
using DTO;
using DTO.BrokerRequests;
using DTO.MarketBrokerObjects;
using Microsoft.AspNetCore.SignalR.Client;

namespace GUI.Scripts
{
    public static class HubConnector
    {
        private static Timer timer;
        public static async Task SubscribeOnCandle(Action<Candle> onReceivedAction, Action onErrorAction, String token, String Figi)
        {

            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5009/CandleHub")
                .Build();

            hubConnection.On<Candle>("ReceiveMessage", Candle =>
            {
                onReceivedAction.Invoke(Candle);
                timer.Stop();
                timer.Dispose();
            });

            await hubConnection.StartAsync();

            await hubConnection.SendAsync("Subscribe", new SubscribeOnCandleRequest
            {
                Token = token,
                Broker = BrokerType.TinkoffBroker,
                Figi = Figi
            });

            timer = new Timer(5000);
            timer.Elapsed += async (source, e) =>
            {
                onErrorAction.Invoke();
                await hubConnection.StopAsync();
                timer.Stop();
                timer.Dispose();
            };
            timer.AutoReset = false;
            timer.Start();
        }
    }
}
