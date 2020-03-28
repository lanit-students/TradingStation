using System;
using System.Globalization;
using System.Xml.Serialization;

namespace CentralBankIntegrationLib.XmlSerializationObjects
{
    /// <summary>
    /// Exchange rate recieved by deserialization from Central Bank.
    /// </summary>
    public class Currency
    {
        // Can't change Accessible levels and type of Value variable because of deserialization
        [XmlElement("NumCode")]
        public string DigitId { get; set; }

        [XmlElement("CharCode")]
        public string CharId { get; set; }

        [XmlElement("Nominal")]
        public int Nominal { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Value")]
        public string StringValue { get; set; }

        public decimal DigitValue
        {
            get
            {
                decimal buff = Convert.ToDecimal(StringValue, CultureInfo.CurrentCulture);
                if (Nominal > 1)
                    return buff / Convert.ToDecimal(Nominal);
                else
                    return buff;                 
            }
        }

        public double GetValueInRUB()
        {
            var valueDouble = Convert.ToDouble(DigitValue);
            var result = Math.Round(valueDouble / Nominal, 2);

            return result;
        }

        public string Description()
        {
            return $"{CharId} - {GetValueInRUB()}";
        }
    }
}
