using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO.NewsRequests.Currency
{
    public class CurrencyRequest
    {
        public CurrencyRequest() { }

        public NewsPublisherTypes CurrecyPublisher { get; set; }
        public List<string> CurrencyCodes { get; set; }
    }
}
