using Newtonsoft.Json;
using System;
using Alexa.NET.Helpers;

namespace Alexa.NET.Request.Type
{
    public abstract class Request
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("timestamp"),JsonConverter(typeof(MixedDateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}