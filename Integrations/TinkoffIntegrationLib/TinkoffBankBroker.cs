using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
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
