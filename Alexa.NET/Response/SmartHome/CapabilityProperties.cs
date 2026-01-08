using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Response.SmartHome
{
    public class CapabilityProperties
    {
        [JsonProperty("supported", NullValueHandling = NullValueHandling.Ignore)]
        public List<CapabilityPropertyName> Supported { get; set; }

        [JsonProperty("proactivelyReported")]
        public bool ProactivelyReported { get; set; }

        [JsonProperty("retrievable")]
        public bool Retrievable { get; set; }
    }
}
