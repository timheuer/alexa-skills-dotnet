using Newtonsoft.Json;
using System;

namespace Alexa.NET.Request.Type
{
    public abstract class Request
    {
        [JsonProperty("type")]
        string Type { get; set; }

        [JsonProperty("requestId")]
        string RequestId { get; set; }

        [JsonProperty("timestamp")]
        DateTime Timestamp { get; set; }
    }
}