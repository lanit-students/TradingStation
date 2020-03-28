using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace GUI.Shared.Currency
{
    public class Currency
    {
        [Parameter]
        [JsonPropertyName("code")]
        public string Id { get; set; }

        [Parameter]
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}
