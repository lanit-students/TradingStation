using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using DTO.BrokerRequests;
using Interfaces;
using Kernel.CustomExceptions;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TinkoffIntegrationLib
{
    public class TinkoffBankBroker : IBroker
    {
        private readonly SandboxContext context;

        public TinkoffBankBroker(string token)
        {
            try
            {
                var conn = ConnectionFactory.GetSandboxConnection(token);
                context = conn.Context;
                context.SetCurrencyBalanceAsync(Currency.Rub, 100000000);
                context.SetCurrencyBalanceAsync(Currency.Usd, 100000000);
                context.SetCurrencyBalanceAsync(Currency.Eur, 100000000);
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }

        public int Depth { get; set; }

        public IEnumerable<Instrument> GetInstruments(DTO.MarketBrokerObjects.InstrumentType type)
        {
            var instruments = new List<Instrument>();

            var tinkoffInstrumentType = (InstrumentType)Enum.Parse(typeof(InstrumentType), type.ToString());

            MarketInstrumentList instrumentsList = tinkoffInstrumentType switch
            {
                InstrumentType.Bond => context.MarketBondsAsync().Result,
                InstrumentType.Currency => context.MarketCurrenciesAsync().Result,
                InstrumentType.Stock => context.MarketStocksAsync().Result,
                _ => throw new BadRequestException()
            };

            Parallel.ForEach(instrumentsList.Instruments,
                (instrument) =>
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

        public bool Trade(InternalTradeRequest request)
        {
            try
            {
                var operation = request.Operation == DTO.MarketBrokerObjects.OperationType.Buy ? OperationType.Buy : OperationType.Sell;
                var order = new LimitOrder(request.Figi, request.Lots, operation, request.Price);
                var result = context.PlaceLimitOrderAsync(order).Result;
                request.DateTime = DateTime.Now;
                request.IsSuccess = result.Status == OrderStatus.Fill ? true : false;
                return true;
            }
            catch
            {
                throw new BadRequestException();
            }

            
        }
    }
}
