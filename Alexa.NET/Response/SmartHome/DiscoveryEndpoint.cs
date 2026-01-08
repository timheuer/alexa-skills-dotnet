using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Response.SmartHome
{
    public class DiscoveryEndpoint
    {
        [JsonProperty("endpointId")]
        public string EndpointId { get; set; }

        [JsonProperty("manufacturerName")]
        public string ManufacturerName { get; set; }

        [JsonProperty("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("displayCategories")]
        public List<string> DisplayCategories { get; set; }

        [JsonProperty("cookie", NullValueHandling = NullValueHandling.Ignore)]
        public object Cookie { get; set; }

        [JsonProperty("capabilities")]
        public List<Capability> Capabilities { get; set; }

        [JsonProperty("additionalAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public AdditionalAttributes AdditionalAttributes { get; set; }
    }
}
