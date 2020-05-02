using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel.CustomExceptions;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;
using TinkoffIntegrationLib.Adapters;
using InstrumentType = DTO.MarketBrokerObjects.InstrumentType;

namespace TinkoffIntegrationLib
{
    public class TinkoffBankBroker : IBroker
    {
        private readonly SandboxContext context;
        private Action<Candle> sendCandle;

        public TinkoffBankBroker(string token)
        {
            try
            {
                var conn = ConnectionFactory.GetSandboxConnection(token);
                context = conn.Context;
                context.RegisterAsync();
                context.SetCurrencyBalanceAsync(Currency.Rub, 10000000000);
                context.SetCurrencyBalanceAsync(Currency.Usd, 10000000000);
                context.SetCurrencyBalanceAsync(Currency.Eur, 10000000000);
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }
        
        public int Depth { get; set; }

        public IEnumerable<Instrument> GetInstruments(InstrumentType type)
        {
            var instruments = new List<Instrument>();

            var tinkoffInstrumentType =
                (Tinkoff.Trading.OpenApi.Models.InstrumentType) Enum.Parse(
                    typeof(Tinkoff.Trading.OpenApi.Models.InstrumentType), type.ToString());

            var instrumentsList = tinkoffInstrumentType switch
            {
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Bond => context.MarketBondsAsync().Result,
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Currency => context.MarketCurrenciesAsync().Result,
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Stock => context.MarketStocksAsync().Result,
                _ => throw new BadRequestException()
            };

            Parallel.ForEach(instrumentsList.Instruments,
                instrument =>
                {
                    try
                    {
                        instruments.Add(
                            new TinkoffInstrumentAdapter(
                                tinkoffInstrumentType,
                                instrument)
                        );
                    }
                    catch (Exception) { }
                });

            return instruments;
        }

        public Transaction Trade(InternalTradeRequest request)
        {
            try
            {
                var transaction = request.Transaction;
                var operation = transaction.Operation == DTO.MarketBrokerObjects.OperationType.Buy ? OperationType.Buy : OperationType.Sell;
                var order = new LimitOrder(transaction.Figi, transaction.Count, operation, transaction.Price);
                var result = context.PlaceLimitOrderAsync(order).Result;
                transaction.DateTime = DateTime.Now;
                transaction.IsSuccess = result.Status == OrderStatus.Fill ? true : false;
                return transaction;
            }
            catch
            {
                throw new BadRequestException("Transaction wasn't complete");
            }
        }
        
        public IEnumerable<Candle> SubscribeOnCandle(string Figi, Action<Candle> SendCandle)
        {
            sendCandle = SendCandle;

            var candles = GetCandles(DateTime.Now, Figi);

            if (candles.Candles.Count == 0)
            {
                var lastDate = DateTime.Now.AddHours(-DateTime.Now.Hour - 5).AddMinutes(-DateTime.Now.Minute);

                int days = 5;

                while (days != 0 && candles.Candles.Count == 0)
                {
                    days -= 1;

                    candles = GetCandles(lastDate, Figi);

                    lastDate = lastDate.AddDays(-1);
                }
            }
            else
            {
                context.SendStreamingRequestAsync(
                    new StreamingRequest.CandleSubscribeRequest(Figi, CandleInterval.Minute));

                context.StreamingEventReceived += OnStreamingEventReceived;
            }

            var candleList = new List<Candle>();

            candles.Candles.ForEach(candle => candleList.Add(new CandleAdapter(candle)));

            return candleList;
        }

        private CandleList GetCandles(DateTime date, string Figi)
        {
            return context.MarketCandlesAsync(Figi, date.AddMinutes(-15), date,
                CandleInterval.Minute).Result;
        }

        private void OnStreamingEventReceived(object sender, StreamingEventReceivedEventArgs args)
        {
            sendCandle(new CandleAdapter(args.Response));
        }
    }
}