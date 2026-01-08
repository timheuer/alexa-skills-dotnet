using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    public class SmartHomeHeader
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("payloadVersion")]
        public string PayloadVersion { get; set; }

        [JsonProperty("correlationToken", NullValueHandling = NullValueHandling.Ignore)]
        public string CorrelationToken { get; set; }
    }
}
