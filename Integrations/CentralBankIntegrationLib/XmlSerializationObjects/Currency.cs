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
        [XmlElement("NumCode")]
        public string NumCode { get; set; }

        [XmlElement("CharCode")]
        public string CharCode { get; set; }

        [XmlElement("Nominal")]
        public int FaceValue { get; set; }

        [XmlElement("Name")]
        public string NameInRussian { get; set; }

        [XmlElement("Value")]
        public string ValueInString { get; set; }

        public decimal ValueInDigits
        {
            get
            {
                decimal val;
                //Todo: Add log
                Decimal.TryParse(ValueInString, out val);

                if (FaceValue > 1)
                    return val / FaceValue;
                else
                    return val;
            }
        }

        public double GetValueInRUB()
        {
            var valueDouble = Convert.ToDouble(ValueInString);
            var result = Math.Round(valueDouble / FaceValue, 2);

            return result;
        }

        public string Description()
        {
            return $"{CharCode} - {GetValueInRUB()}";
        }
    }
}
