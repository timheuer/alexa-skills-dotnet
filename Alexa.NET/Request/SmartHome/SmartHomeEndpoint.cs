using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    public class SmartHomeEndpoint
    {
        [JsonProperty("endpointId", NullValueHandling = NullValueHandling.Ignore)]
        public string EndpointId { get; set; }

        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public Scope Scope { get; set; }

        [JsonProperty("cookie", NullValueHandling = NullValueHandling.Ignore)]
        public object Cookie { get; set; }
    }
}
