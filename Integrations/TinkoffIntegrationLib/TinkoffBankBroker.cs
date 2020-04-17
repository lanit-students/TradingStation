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

        private List<IMarketInstrument> GetInstruments(Task<MarketInstrumentList> getInstrumentsTask, Context context, int depth)
        {
            List<IMarketInstrument> instruments = new List<IMarketInstrument>();

            MarketInstrumentList instrumentsList = getInstrumentsTask.Result;

            for(int i = 0; i< instrumentsList.Total; i++)
            {
                Orderbook orderbook = context
                    .MarketOrderbookAsync(instrumentsList.Instruments[i].Figi
                    , depth).Result;

                instruments.Add(new TinkoffInstrumentAdapter(instrumentsList.Instruments[i],
                    orderbook));
            }

            return instruments;
        }


        public TinkoffBankBroker(BrokerData data)
        {
            Connection conn = ConnectionFactory.GetConnection(data.Token);
            tinkoffContext = conn.Context;
            if (data.Depth == 0)
                Depth = CDefaultDepth;
            else
                Depth = data.Depth;
        }


        /*
         * After create broker, if you want receive responce to request
         * with another depth, you must change depth here and make
         * a request again
        */
        /// <summary>
        /// Depth by market glass
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// Method returns specific bond
        /// </summary>
        /// <param name="idBond">Id required bond, e.g. figi</param>
        /// <returns>Required bond</returns>
        public IMarketInstrument GetBond(string idBond)
        {
            return GetAllBonds()
                .Where(b => b.Id == idBond)
                .Single();
        }

        /// <summary>
        /// Method returns list with all bonds from bank broker
        /// </summary>
        /// <returns>List with bonds</returns>
        public List<IMarketInstrument> GetAllBonds()
        {
            return GetInstruments(tinkoffContext.MarketBondsAsync(), tinkoffContext, Depth);
        }

        /// <summary>
        /// Method returns list with all curencies from bank broker
        /// </summary>
        /// <returns>List with currencies</returns>
        public List<IMarketInstrument> GetAllCurrencies()
        {
            return GetInstruments(tinkoffContext.MarketCurrenciesAsync(), tinkoffContext, Depth);
        }

        /// <summary>
        /// Method returns specific currency
        /// </summary>
        /// <param name="idCurrency">Id required currency, e.g. figi</param>
        /// <returns>Required currency</returns>
        public IMarketInstrument GetCurrency(string idCurrency)
        {
            return GetAllCurrencies()
                .Where(c => c.Id == idCurrency)
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
                .Where(st => st.Id == idStock)
                .Single();
        }

        /// <summary>
        /// Method returns list with all stocks from bank broker
        /// </summary>
        /// <returns>List with stocks</returns>
        public List<IMarketInstrument> GetAllStocks()
        {
            return GetInstruments(tinkoffContext.MarketStocksAsync(), tinkoffContext, Depth);
        }
    }
}
