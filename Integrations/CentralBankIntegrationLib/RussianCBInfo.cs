using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;

using CentralBankIntegrationLib.XmlSerializationObjects;

using DTO.NewsRequests.Currency;
using Interfaces;

namespace CBIntegration
{
    /// <summary>
    /// Get reference information from Central Bank.
    /// </summary>
    public class RussianCBInfo : ICurrencyExchangeRateProvider
    {
        private DataFromCB data;
        private Uri uri;

        public RussianCBInfo(Uri uri)
        {
            this.uri = uri;
            data = GetData(uri);
        }

        // Workaround
        public RussianCBInfo() : this(new Uri("http://www.cbr.ru/scripts/XML_daily.asp")) { }

        public List<ExchangeRate> GetCurrencies()
        {
            var rates = new List<ExchangeRate>();
            for (int i = 0; i < data.Currencies.Count; i++)
            {
                rates.Add(new ExchangeRate
                {
                    Code = data[i].CharCode, 
                    Value = decimal.Round(data[i].ValueInDigits,2)
                });
            }
            return rates;
        }

        // Connect to Central Bank Exchange Rates, get the most popular valutes.
        private DataFromCB GetData(Uri uri)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using Stream responseStream = httpWebResponse.GetResponseStream();
            using var streamReader = new StreamReader(responseStream);

            var xmlSerializer = new XmlSerializer(typeof(DataFromCB));

            return (DataFromCB)xmlSerializer.Deserialize(streamReader);
        }
    }
}
