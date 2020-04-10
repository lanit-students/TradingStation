using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.NewsRequests.Currency
{
    public class CurrencyRequest
    {
        public CurrencyRequest() { }

        [JsonPropertyName("bank_name")]
        public CurrencyExchangeRateProviderTypes CurrencyPublisher { get; set; }

        [JsonPropertyName("currency_codes")]
        public List<string> CurrencyCodes { get; set; }
    }
}
