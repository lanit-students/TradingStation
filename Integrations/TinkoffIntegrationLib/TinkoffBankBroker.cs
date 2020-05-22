using System;
using System.Collections.Concurrent;
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
        private const decimal CmaxBalance = decimal.MaxValue - 1;

        public TinkoffBankBroker(string token)
        {
            try
            {
                var conn = ConnectionFactory.GetSandboxConnection(token);
                context = conn.Context;
                context.RegisterAsync();
                context.SetCurrencyBalanceAsync(Currency.Rub, CmaxBalance);
                context.SetCurrencyBalanceAsync(Currency.Usd, CmaxBalance);
                context.SetCurrencyBalanceAsync(Currency.Eur, CmaxBalance);
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }

        private List<Instrument> ParseInstruments(List<MarketInstrument> marketInstruments, Tinkoff.Trading.OpenApi.Models.InstrumentType type)
        {
            var res = new ConcurrentBag<Instrument>();

            Parallel.ForEach(marketInstruments,
                instrument =>
                {
                    res.Add(
                            new TinkoffInstrumentAdapter(
                                type,
                                instrument)
                        );
                });

            return res.ToList();
        }

        public IEnumerable<Instrument> GetInstruments(InstrumentType type)
        {
            if (type == InstrumentType.Any)
            {
                var bonds = context.MarketBondsAsync().Result.Instruments;
                var currencies = context.MarketCurrenciesAsync().Result.Instruments;
                var stocks = context.MarketStocksAsync().Result.Instruments;

                return
                    ParseInstruments(bonds, Tinkoff.Trading.OpenApi.Models.InstrumentType.Bond)
                    .Concat(ParseInstruments(currencies, Tinkoff.Trading.OpenApi.Models.InstrumentType.Currency))
                    .Concat(ParseInstruments(stocks, Tinkoff.Trading.OpenApi.Models.InstrumentType.Stock));
            }

            var tinkoffInstrumentType =
                (Tinkoff.Trading.OpenApi.Models.InstrumentType)Enum.Parse(
                    typeof(Tinkoff.Trading.OpenApi.Models.InstrumentType), type.ToString());

            var instruments = tinkoffInstrumentType switch
            {
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Bond => context.MarketBondsAsync().Result.Instruments,
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Currency => context.MarketCurrenciesAsync().Result.Instruments,
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Stock => context.MarketStocksAsync().Result.Instruments,
                _ => throw new BadRequestException()
            };

            return ParseInstruments(instruments, tinkoffInstrumentType);
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

        public IEnumerable<Candle> SubscribeOnCandle(string Figi, int timeInterval, Action<Candle> SendCandle)
        {
            sendCandle = SendCandle;

            var candles = GetCandles(DateTime.Now, timeInterval, Figi);

            if (candles.Candles.Count == 0)
            {
                var lastDate = DateTime.Now.AddHours(-DateTime.Now.Hour - 5).AddMinutes(-DateTime.Now.Minute);

                int days = 5;

                while (days != 0 && candles.Candles.Count == 0)
                {
                    days -= 1;

                    candles = GetCandles(lastDate, timeInterval, Figi);

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

        private CandleList GetCandles(DateTime date, int timeInterval, string Figi)
        {
            return context.MarketCandlesAsync(Figi, date.AddMinutes(-timeInterval), date,
                CandleInterval.Minute).Result;
        }

        private void OnStreamingEventReceived(object sender, StreamingEventReceivedEventArgs args)
        {
            sendCandle(new CandleAdapter(args.Response));
        }
    }
}