using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces;
using Kernel.CustomExceptions;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TinkoffIntegrationLib
{
    public class TinkoffBankBroker : IBroker
    {
        private readonly Context context;

        public TinkoffBankBroker(string token, int depth = 10)
        {
            try
            {
                var conn = ConnectionFactory.GetSandboxConnection(token);
                context = conn.Context;

                Depth = depth;
            }
            catch (Exception)
            {
                throw new BadRequestException();
            }
        }

        /*
         * After creating the broker, if you want receive response to request
         * with another depth, you must change depth here and make
         * a request again
        */
        /// <summary>
        /// Depth of market glass
        /// </summary>
        public int Depth { get; set; }

        public IEnumerable<IMarketInstrument> GetInstruments(InstrumentType type)
        {
            var instruments = new List<IMarketInstrument>();

            MarketInstrumentList instrumentsList = type switch
            {
                InstrumentType.Bond => context.MarketBondsAsync().Result,
                InstrumentType.Currency => context.MarketCurrenciesAsync().Result,
                InstrumentType.Stock => context.MarketStocksAsync().Result,
                _ => throw new NotImplementedException()
            };

            Parallel.ForEach(instrumentsList.Instruments,
                (instrument) =>
                {
                    try
                    {
                        instruments.Add(
                            new TinkoffInstrumentAdapter(
                                type,
                                instrument,
                                context.MarketOrderbookAsync(
                                    instrument.Figi,
                                    Depth).Result)
                            );
                    }
                    catch (Exception) { }
                });

            return instruments;
        }
    }
}
