using System.Collections.Generic;
using System.Xml.Serialization;

namespace CentralBankIntegrationLib.XmlSerializationObjects
{
    /// <summary>
    /// This class supposed to deserialise information about exchange rate from central bank website.
    /// Example of objects : http://www.cbr.ru/scripts/XML_daily.asp?date_req=02/03/2002
    /// </summary>
    [XmlRoot("ValCurs")]
    public class ValCurs
    {
        [XmlElement("Valute")]
        public List<Valute> Valutes { get; set; }

        public List<string> TopValutesCharCode => new List<string> { "EUR", "USD", "GBR", "CNY", "JPY" };
    }
}
