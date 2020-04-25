using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Tinkoff.Trading.OpenApi.Models;

namespace TinkoffIntegrationLib.Adapters
{
    class CandleAdapter : Candle
    {
        private readonly CandlePayload candleResponse;

        public CandleAdapter(CandlePayload candleResponse)
        {
            this.candleResponse = candleResponse;
        }

        public CandleAdapter() { }

        public decimal Open => candleResponse.Open;

        public decimal Close => candleResponse.Close;

        public decimal High => candleResponse.High;

        public decimal Low => candleResponse.Low;

        public decimal Volume => candleResponse.Volume;

        public DateTime Time => candleResponse.Time;

        public string Figi => candleResponse.Figi;
    }
}
