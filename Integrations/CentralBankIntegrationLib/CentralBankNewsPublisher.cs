using Interfaces;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using CentralBankIntegrationLib.XmlSerializationObjects;

namespace CentralBankIntegration
{
    /// <summary>
    /// Get reference information from Central Bank.
    /// </summary>
    public class CentralBankNewsPublisher : INewsPublisher
    {
        public string GetNews()
        {
            var valutes = getTopValutes();
            StringBuilder sb = new StringBuilder();
            foreach (Valute valute in valutes)
            {
                sb.AppendLine(valute.Description());
            }
            return sb.ToString();
        }

        // Connect to Central Bank Exchange Rates, get the most popular valutes.
        private List<Valute> getTopValutes()
        {
            var url = "http://www.cbr.ru/scripts/XML_daily.asp";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            var top5Valutes = new List<Valute>();
            
            using (StreamReader streamReader = new StreamReader(responseStream))
            {
                var xmlSerializer = new XmlSerializer(typeof(ValCurs));
                var valCurs = (ValCurs)xmlSerializer.Deserialize(streamReader);
                var topValutesCharCode = valCurs.GetTopValutesCharCode;

                foreach(Valute valute in valCurs.ValutesList)
                {
                    if (topValutesCharCode.Contains(valute.CharCode))
                    {
                        top5Valutes.Add(valute);
                    }
                }

                return top5Valutes;
            }
        }
    }
}
