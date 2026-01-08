using Newtonsoft.Json;
using Alexa.NET.Request.SmartHome;

namespace Alexa.NET.Response.SmartHome
{
    public class SmartHomeEvent
    {
        [JsonProperty("header")]
        public SmartHomeHeader Header { get; set; }

        [JsonProperty("endpoint", NullValueHandling = NullValueHandling.Ignore)]
        public SmartHomeEndpoint Endpoint { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }
    }
}
