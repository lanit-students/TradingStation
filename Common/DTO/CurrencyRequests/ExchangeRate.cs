using System.Text.Json.Serialization;

namespace DTO.CurrencyRequests
{
    public class ExchangeRate
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
