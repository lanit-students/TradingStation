using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Interfaces;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;

namespace TinkoffIntegrationLib
{
    public class TinkoffBankBroker : IBroker
    {
        private readonly Context tinkoffContext;
        private const int CDefaultDepth = 10;

        public TinkoffBankBroker(CreateBrokerData data)
        {
            var conn = ConnectionFactory.GetSandboxConnection(data.Token);
            tinkoffContext = conn.Context;

            if (data.Depth == 0)
            {
                Depth = CDefaultDepth;
            }
            else
            {
                Depth = data.Depth;
            }
        }

        /*
         * After creating the broker, if you want receive response to request
         * with another depth, you must change depth here and make
         * a request again
        */
        /// <summary>
        /// Depth by market glass
        /// </summary>
        public int Depth { get; set; }

        public List<IMarketInstrument> GetInstruments(InstrumentType type, int depth)
        {
            var instruments = new List<IMarketInstrument>();

            MarketInstrumentList instrumentsList = type switch
            {
                InstrumentType.Bond => tinkoffContext.MarketBondsAsync().Result,
                InstrumentType.Currency => tinkoffContext.MarketCurrenciesAsync().Result,
                InstrumentType.Stock => tinkoffContext.MarketStocksAsync().Result,
                _ => throw new NotImplementedException()
            };

            for (int i = 0; i < instrumentsList.Total; i++)
            {
                // I'm not sure abpout this try/catch, but Tinkoff
                // keeps sending weird null ref exceptions from time to time
                try
                {
                    Orderbook orderbook = tinkoffContext
                    .MarketOrderbookAsync(instrumentsList.Instruments[i].Figi
                    , depth).Result;

                    instruments.Add(new TinkoffInstrumentAdapter(type,
                        instrumentsList.Instruments[i], orderbook));
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return instruments;
        }

        public async Task SaveInstrument(IMarketInstrument instrument)
        {
            var portfolio = await tinkoffContext.PortfolioAsync();

            var position = new Portfolio.Position(
                instrument.Figi,
                instrument.Ticker,
                instrument.Isin,
                instrument.Type,
                0,
                0,
                new MoneyAmount(instrument.Currency, 0),
                0,
                new MoneyAmount(instrument.Currency, 0),
                new MoneyAmount(instrument.Currency, 0));

            portfolio.Positions.Add(position);
        }

        /// <summary>
        /// Method returns specific bond
        /// </summary>
        /// <param name="idBond">Id required bond, e.g. figi</param>
        /// <returns>Required bond</returns>
        public IMarketInstrument GetBond(string idBond)
        {
            return GetAllBonds()
                .Where(b => b.Figi == idBond)
                .Single();
        }

        /// <summary>
        /// Method returns list with all bonds from bank broker
        /// </summary>
        /// <returns>List with bonds</returns>
        public List<IMarketInstrument> GetAllBonds()
        {
            return GetInstruments(InstrumentType.Bond, Depth);
        }

        /// <summary>
        /// Method returns list with all curencies from bank broker
        /// </summary>
        /// <returns>List with currencies</returns>
        public List<IMarketInstrument> GetAllCurrencies()
        {
            return GetInstruments(InstrumentType.Currency, Depth);
        }

        /// <summary>
        /// Method returns specific currency
        /// </summary>
        /// <param name="idCurrency">Id required currency, e.g. figi</param>
        /// <returns>Required currency</returns>
        public IMarketInstrument GetCurrency(string idCurrency)
        {
            return GetAllCurrencies()
                .Where(c => c.Figi == idCurrency)
                .Single();
        }

        /// <summary>
        /// Method returns specific stock
        /// </summary>
        /// <param name="idStock">Id required stock, e.g. figi</param>
        /// <returns>Required stock</returns>
        public IMarketInstrument GetStock(string idStock)
        {
            return GetAllStocks()
                .Where(st => st.Figi == idStock)
                .Single();
        }

        /// <summary>
        /// Method returns list with all stocks from bank broker
        /// </summary>
        /// <returns>List with stocks</returns>
        public List<IMarketInstrument> GetAllStocks()
        {
            return GetInstruments(InstrumentType.Stock, Depth);
        }
    }
}
