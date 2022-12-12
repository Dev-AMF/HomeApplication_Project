using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace _0_Framework.Application
{
    public class TokenResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("code")]
        public int? ErrorCode { get; set; }

        [JsonPropertyName("transid")]
        public string TransactionId { get; set; }
    }
}
