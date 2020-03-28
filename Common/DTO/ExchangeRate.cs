using System.Text.Json.Serialization;

namespace DTO
{
    public class ExchangeRate
    {
        public ExchangeRate() { }

        [JsonPropertyName("code")]
        public string CurrencyCode { get; set; }
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
