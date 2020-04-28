using DTO;
using DTO.MarketBrokerObjects;
using DTO.RestRequests;
using Kernel;
using Kernel.Enums;
using System;
using System.Collections.Generic;
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
            const string url = "https://localhost:5009/operations/instruments/get";

            var queryParams = new Dictionary<string, string>
            {
                { "bank", broker.ToString() },
                { "token", token },
                { "instrument", instrument.ToString() }
            };

            var client = new RestClient<object, IEnumerable<Instrument>>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }

        public static async Task<bool> Trade (TradeRequest request)
        {
            const string url = "https://localhost:5009/operations/trade";

            var client = new RestClient<object, bool>(url, RestRequestType.POST);

            return await client.Execute(request);
        }

        public static async Task<Instrument> GetInstrumentFromPortfolio(Guid userId, string figi)
        {
            const string url = "https://localhost:5009/operations/instrument/getFromPortfolio";

            var queryParams = new Dictionary<string, string>
            {
                { "userId",userId.ToString() },
                { "figi", figi },
            };

            var client = new RestClient<object, Instrument>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }

        public static async Task<BrokerUser> GetBrokerUser(Guid userId, BrokerType broker)
        {
            const string url = "https://localhost:5009/operations/brokerUser/get";

            var queryParams = new Dictionary<string, string>
            {
                { "userId", userId.ToString() },
                { "broker", broker.ToString()}
            };

            var client = new RestClient<object, BrokerUser>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }

        public static async Task <bool> UpdateBrokerUser(UpdateBrokerUserRequest request)
        {
            const string url = "https://localhost:5009/operations/brokerUser/update";

            var client = new RestClient<object, bool>(url, RestRequestType.PUT);

            return await client.Execute(request);
        }
    }
}