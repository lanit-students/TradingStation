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

        private static List<IMarketInstrument> GetInstruments(Task<MarketInstrumentList> getInstrumentsTask, Context context, int depth)
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

        public TinkoffBankBroker(CreateBrokerData data)
        {
            Connection conn = ConnectionFactory.GetConnection(data.Token);
            tinkoffContext = conn.Context;
            if (data.Depth == 0)
                Depth = CDefaultDepth;
            else
                Depth = data.Depth;
        }

        public int Depth { get; set; }

        public IMarketInstrument GetBond(string idBond)
        {
            return GetBonds()
                .Where(b => b.Id == idBond)
                .Single();
        }

        public List<IMarketInstrument> GetBonds()
        {
            return GetInstruments(tinkoffContext.MarketBondsAsync(), tinkoffContext, Depth);
        }

        public List<IMarketInstrument> GetCurrencies()
        {
            return GetInstruments(tinkoffContext.MarketCurrenciesAsync(), tinkoffContext, Depth);
        }

        public IMarketInstrument GetCurrency(string idCurrency)
        {
            return GetCurrencies()
                .Where(c => c.Id == idCurrency)
                .Single();
        }

        public IMarketInstrument GetStock(string idStock)
        {
            return GetStocks()
                .Where(st => st.Id == idStock)
                .Single();
        }

        public List<IMarketInstrument> GetStocks()
        {
            return GetInstruments(tinkoffContext.MarketStocksAsync(), tinkoffContext, Depth);
        }
    }
}
