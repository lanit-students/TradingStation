using DTO;
using DTO.MarketBrokerObjects;
using Kernel;
using Kernel.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class BrokerManager
    {
        public static async Task<IEnumerable<Instrument>> GetInstruments(
                BrokerType broker,
                string token,
                InstrumentType instrument,
                int depth = 10)
        {
            const string url = "https://localhost:5009/operations/instruments/get";

            var queryParams = new Dictionary<string, string>
            {
                { "bank", broker.ToString() },
                { "token", token },
                { "depth", depth.ToString() },
                { "instrument", instrument.ToString() }
            };

            var client = new RestClient<object, IEnumerable<Instrument>>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }

        public static async Task<Instrument> GetInstrument(BrokerType broker, string token, string figi)
        {
            const string url = "https://localhost:5009/operations/instruments/getInstrument";

            var queryParams = new Dictionary<string, string>
            {
                { "broker", broker.ToString() },
                { "figi", figi},
                { "token", token },
            };

            var client = new RestClient<object, Instrument>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }
    }
}