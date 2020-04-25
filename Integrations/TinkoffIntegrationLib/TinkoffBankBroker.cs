using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Interfaces;
using Kernel.CustomExceptions;
using Tinkoff.Trading.OpenApi.Models;
using Tinkoff.Trading.OpenApi.Network;
using TinkoffIntegrationLib.Adapters;
using InstrumentType = DTO.MarketBrokerObjects.InstrumentType;

namespace TinkoffIntegrationLib
{
    public class TinkoffBankBroker : IBroker
    {
        private readonly Context context;
        private Action<Candle> sendCandle;

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
        ///     Depth of market glass
        /// </summary>
        public int Depth { get; set; }

        public IEnumerable<Instrument> GetInstruments(InstrumentType type)
        {
            var instruments = new List<Instrument>();

            var tinkoffInstrumentType =
                (Tinkoff.Trading.OpenApi.Models.InstrumentType) Enum.Parse(
                    typeof(Tinkoff.Trading.OpenApi.Models.InstrumentType), type.ToString());

            var instrumentsList = tinkoffInstrumentType switch
            {
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Bond => context.MarketBondsAsync().Result,
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Currency => context.MarketCurrenciesAsync().Result,
                Tinkoff.Trading.OpenApi.Models.InstrumentType.Stock => context.MarketStocksAsync().Result,
                _ => throw new BadRequestException()
            };

            Parallel.ForEach(instrumentsList.Instruments,
                instrument =>
                {
                    try
                    {
                        instruments.Add(
                            new TinkoffInstrumentAdapter(
                                tinkoffInstrumentType,
                                instrument)
                        );
                    }
                    catch (Exception)
                    {
                    }
                });

            return instruments;
        }


        public void SubscribeOnCandle(string Figi, Action<Candle> SendCandle)
        {
            this.sendCandle = SendCandle;
            context.StreamingEventReceived += onStreamingEventReceived;
            context.SendStreamingRequestAsync(new StreamingRequest.CandleSubscribeRequest(Figi, CandleInterval.Minute));
        }


        private void onStreamingEventReceived(object sender, StreamingEventReceivedEventArgs args)
        {
            if (args.Response.Event.Equals("Error"))
            {
                sendCandle(new CandleAdapter{isValid = false});
            }
            else
            {
                var response = (CandleResponse) args.Response;

                sendCandle(new CandleAdapter(response.Payload));
            }
        }
    }
}