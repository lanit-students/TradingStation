using Interfaces;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using CentralBankIntegrationLib.XmlSerializationObjects;
using System;
using DTO;

namespace CentralBankIntegration
{
    /// <summary>
    /// Get reference information from Central Bank.
    /// </summary>
    public class CentralBankCurrenciesPublisher : INewsPublisher
    {
        const string uri = "http://www.cbr.ru/scripts/XML_daily.asp";
        DataFromCentralBank data;

        public CentralBankCurrenciesPublisher(Uri uriBank)
        {
            data = GetData(uriBank);
        }

        public CentralBankCurrenciesPublisher() : this(new Uri(uri)) { }

        public List<ExchangeRate> GetCurrencies()
        {
            List<ExchangeRate> rates = new List<ExchangeRate>();
            //Parallel.For(0, data.Currencies.Count, (int i) =>
            //{
            //    rates.Add(new ExchangeRate { CurrencyCode = data[i].CharId, Value = data[i].DigitValue });
            //});

            for(int i = 0; i < data.Currencies.Count; i++)
            {
                rates.Add(new ExchangeRate { CurrencyCode = data[i].CharId, Value = data[i].DigitValue });
            }

            return rates;
        }

        // Connect to Central Bank Exchange Rates, get the most popular valutes.
        private DataFromCentralBank GetData(Uri uri)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            DataFromCentralBank data;
            XmlSerializer serializer = new XmlSerializer(typeof(DataFromCentralBank));

            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(responseStream);
                data = (DataFromCentralBank) serializer.Deserialize(streamReader);
            }
            
            //Todo: past exception
            //if (data == nul) {}

            return data;
        }
    }
}
