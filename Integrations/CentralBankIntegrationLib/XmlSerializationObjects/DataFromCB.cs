using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CentralBankIntegrationLib.XmlSerializationObjects
{
    /// <summary>
    /// This class supposed to deserialise information about exchange rate from central bank website.
    /// Example of objects : http://www.cbr.ru/scripts/XML_daily.asp?date_req=02/03/2002
    /// </summary>
    [XmlRoot("ValCurs")]
    public class DataFromCB
    {
        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("name")]
        public string NameData { get; set; }

        [XmlElement("Valute")]
        public List<Currency> Currencies { get; set; }

        public Currency this[int index]
        {
            get => Currencies[index];
        }

    }
}
