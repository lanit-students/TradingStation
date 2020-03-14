using System;

namespace CentralBankIntegrationLib.XmlSerializationObjects
{
    /// <summary>
    /// Exchange rate recieved by deserialization from Central Bank.
    /// </summary>
    public class Valute
    {
        // Can't change Accessible levels and type of Value variable because of deserialization
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public double GetValueInRUB()
        {
            var valueDouble = Convert.ToDouble(Value);
            var result = Math.Round(valueDouble / Nominal, 2);
            return result;
        }

        public string Description()
        {
            return $"{CharCode} - {GetValueInRUB()}";
        }

    }

}
