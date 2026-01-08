using Newtonsoft.Json;

namespace Alexa.NET.Response.SmartHome
{
    public class Capability
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("interface")]
        public string Interface { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public CapabilityProperties Properties { get; set; }

        [JsonProperty("capabilityResources", NullValueHandling = NullValueHandling.Ignore)]
        public object CapabilityResources { get; set; }

        [JsonProperty("configuration", NullValueHandling = NullValueHandling.Ignore)]
        public object Configuration { get; set; }
    }
}
