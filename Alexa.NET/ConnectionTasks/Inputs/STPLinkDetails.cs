using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPLinkDetails
    {
        [JsonProperty("primary",NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SendToPhoneLinkConverter))]
        public ISendToPhoneLink Primary { get; set; }

        [JsonProperty("fallback",NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(SendToPhoneLinkConverter))]
        public ISendToPhoneFallbackLink Fallback { get; set; }
    }
}