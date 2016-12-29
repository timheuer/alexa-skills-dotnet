using Newtonsoft.Json;
using System;

namespace Alexa.NET.Request.Type
{
    public abstract class Request
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}