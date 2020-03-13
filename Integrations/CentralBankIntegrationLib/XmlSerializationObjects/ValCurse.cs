using System.Collections.Generic;
using System.Xml.Serialization;

namespace CentralBankIntegrationLib.XmlSerializationObjects
{
    /// <summary>
    /// This class supposed to deserialise information about exchange rate from central bank website.
    /// Example of objects : http://www.cbr.ru/scripts/XML_daily.asp?date_req=02/03/2002
    /// </summary>
    /// 

    [XmlRootAttribute("ValCurs")]
    public class ValCurs
    {
        [XmlElement("Valute")]

        public List<Valute> ValutesList { get; set; }
        
        private readonly List<string> topValutesCharCode = new List<string>{ "EUR", "USD", "GBP", "CNY", "JPY" };
        
        public List<string> GetTopValutesCharCode
        {
            get => topValutesCharCode;
        }
        }
    }

