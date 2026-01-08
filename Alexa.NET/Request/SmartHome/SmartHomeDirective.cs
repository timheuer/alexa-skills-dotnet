using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    public class SmartHomeDirective
    {
        [JsonProperty("header")]
        public SmartHomeHeader Header { get; set; }

        [JsonProperty("endpoint", NullValueHandling = NullValueHandling.Ignore)]
        public SmartHomeEndpoint Endpoint { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}
