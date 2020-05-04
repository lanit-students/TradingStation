using System;
using DTO;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffIntegrationLib.Adapters
{
    internal class CandleAdapter : Candle
    {
        private readonly CandlePayload candlePayload;

        public CandleAdapter(StreamingResponse candleResponse)
        {
            try
            {
                candlePayload = ((CandleResponse) candleResponse).Payload;
            }
            catch (Exception e)
            {
                candlePayload = new CandlePayload(0, 0, 0, 0, 0, DateTime.Now, CandleInterval.Minute, "0");
            }
        }

        public CandleAdapter(CandlePayload candlePayload)
        {
            this.candlePayload = candlePayload;
        }

        public override decimal Open => candlePayload.Open;

        public override decimal Close => candlePayload.Close;

        public override decimal High => candlePayload.High;

        public override decimal Low => candlePayload.Low;

        public override decimal Volume => candlePayload.Volume;

        public override DateTime Time => candlePayload.Time;

        public override string Figi => candlePayload.Figi;
    }
}