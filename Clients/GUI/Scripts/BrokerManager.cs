using DTO;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class BrokerManager
    {
        public static async Task<IEnumerable<Instrument>> GetInstruments(
                BrokerType broker,
                string token,
                InstrumentType instrument
            )
        {
            const string url = "http://localhost:5008/operations/instruments/get";

            var queryParams = new Dictionary<string, string>
            {
                { "bank", broker.ToString() },
                { "token", token },
                { "instrument", instrument.ToString() }
            };

            var client = new RestClient<object, IEnumerable<Instrument>>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.ExecuteAsync();
        }

        public static async Task<bool> Trade (TradeRequest request)
        {
            const string url = "http://localhost:5008/operations/trade";

            var client = new RestClient<object, bool>(url, RestRequestType.POST);

            return await client.ExecuteAsync(request);
        }

        public static async Task<Instrument> GetInstrumentFromPortfolio(Guid userId, string figi)
        {
            const string url = "http://localhost:5008/operations/instrument/getFromPortfolio";

            var queryParams = new Dictionary<string, string>
            {
                { "userId",userId.ToString() },
                { "figi", figi },
            };

            var client = new RestClient<object, Instrument>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.ExecuteAsync();
        }

        public static async Task<Instrument> GetInstrument(string figi, string tinkoffToken)
        {
            var instruments = await GetInstruments(BrokerType.TinkoffBroker, tinkoffToken, InstrumentType.Any);

            return instruments.FirstOrDefault(x => x.Figi == figi);
        }

        public static async Task<List<InstrumentData>> GetPortfolio(UserToken userToken, string tinkoffToken)
        {
            const string url = "https://localhost:5009/operations/getportfolio";

            var client = new RestClient<object, List<InstrumentData>>(url, RestRequestType.GET, userToken);

            var portfolio = await client.ExecuteAsync();

            var instruments = await GetInstruments(BrokerType.TinkoffBroker, tinkoffToken, InstrumentType.Any);

            foreach (var instrumentData in portfolio)
            {
                try
                {
                    var instrument = instruments.First(x => x.Figi == instrumentData.Figi);
                    instrumentData.Name = instrument.Name;
                    instrumentData.Currency = instrument.Currency;
                }
                catch
                {
                    instrumentData.Name = "Name unavailable";
                }
            }

            return portfolio;
        }

        public static async Task<UserBalance> GetUserBalance(Guid userId)
        {
            const string url = "http://localhost:5008/operations/userBalance/get";

            var queryParams = new Dictionary<string, string>
            {
                { "userId", userId.ToString() }
            };

            var client = new RestClient<object, UserBalance>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.ExecuteAsync();
        }

        public static async Task <bool> UpdateUserBalance(UpdateUserBalanceRequest request)
        {
            const string url = "http://localhost:5008/operations/userBalance/update";

            var client = new RestClient<object, bool>(url, RestRequestType.PUT);

            return await client.ExecuteAsync(request);
        }

        public static async Task<IEnumerable<Transaction>> GetTransactions(Guid userId)
        {
            const string url = "http://localhost:5008/operations/transactions/get";

            var queryParams = new Dictionary<string, string>
            {
                { "userId", userId.ToString() }
            };

            var client = new RestClient<object, IEnumerable<Transaction>>(url, RestRequestType.GET, queryParams : queryParams);

            return await client.ExecuteAsync();
        }
    }
}