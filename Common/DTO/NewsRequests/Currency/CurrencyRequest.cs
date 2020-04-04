using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.NewsRequests.Currency
{
    public class CurrencyRequest
    {
        [JsonPropertyName("bank_name")]
        public NewsPublisherTypes CurrecyPublisher { get; set; }

        [JsonPropertyName("currency_codes")]
        public List<string> CurrencyCodes { get; set; }
    }
}
