using Newtonsoft.Json;
using System;

namespace Alexa.NET.Request.Type
{
    public interface IRequest
    {
        [JsonProperty("type")]
        string Type { get; set; }

        [JsonProperty("requestId")]
        string RequestId { get; set; }

        [JsonProperty("timestamp")]
        DateTime Timestamp { get; set; }
    }
}