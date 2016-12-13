using Alexa.NET.Request.Type;
using Newtonsoft.Json;
using System;

namespace Alexa.NET.Request
{
    public class RequestBundle : IIntentRequest, ILaunchRequest, ISessionEndedRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("intent")]
        public Intent Intent { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        public System.Type GetRequestType()
        {
            switch (Type)
            {
                case "IntentRequest":
                    return typeof(IIntentRequest);
                case "LaunchRequest":
                    return typeof(ILaunchRequest);
                case "SessionEndedRequest":
                    return typeof(ISessionEndedRequest);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), $"Unknown request type: {Type}.");
            }
        }
    }
}