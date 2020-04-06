using Interfaces;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using CentralBankIntegrationLib.XmlSerializationObjects;
using System.Linq;
using System;

namespace CentralBankIntegration
{
    /// <summary>
    /// Get reference information from Central Bank.
    /// </summary>
    public class CentralBankNewsPublisher : INewsPublisher
    {
        public string GetNews()
        {
            List<Valute> valutes = GetTopValutes();

            var sb = new StringBuilder();
            try
            {
                if (valutes.Count == 0)
                {
                    sb.AppendLine("No currencies found");
                }
                else
                {
                    sb.AppendLine("Top - 5 exchange rates:");
                    foreach (var valute in valutes)
                    {
                        var description = valute.Description();
                        sb.AppendLine(description);
                    }
                }
            }
            catch (NullReferenceException)
            {
                sb.AppendLine("No currencies found");
            }
            

            return sb.ToString();
        }

        // Connect to Central Bank Exchange Rates, get the most popular valutes.
        private List<Valute> GetTopValutes()
        {
            var url = "http://www.cbr.ru/scripts/XML_daily.asp";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader streamReader = new StreamReader(responseStream);

            var top5Valutes = new List<Valute>();

            var xmlSerializer = new XmlSerializer(typeof(ValCurs));

            var valCurs = (ValCurs)xmlSerializer.Deserialize(streamReader);

            return valCurs?.Valutes?.Where(v => valCurs.TopValutesCharCode.Contains(v.CharCode)).ToList();
        }
    }
}
