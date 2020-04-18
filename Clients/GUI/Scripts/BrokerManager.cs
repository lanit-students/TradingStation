using DTO;
using Kernel;
using Kernel.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Scripts
{
    public static class BrokerManager
    {
        public static async Task<IEnumerable<Instrument>> GetInstruments(BankType bank, string token, string instrument, int depth = 10)
        {
            const string url = "https://localhost:5003/brokers/instruments/get";

            var queryParams = new Dictionary<string, string>
            {
                { "bank", bank.ToString() },
                { "token", token },
                { "depth", depth.ToString() },
                { "instrument", instrument }
            };

            var client = new RestClient<object, IEnumerable<Instrument>>(url, RestRequestType.GET, queryParams: queryParams);

            return await client.Execute();
        }
    }
}