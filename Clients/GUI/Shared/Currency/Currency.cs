using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
