using System.Text.Json.Serialization;

namespace DTO.NewsRequests.Currency
{
    public class ExchangeRate
    {
        public ExchangeRate() { }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
