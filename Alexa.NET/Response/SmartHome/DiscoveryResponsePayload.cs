using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Response.SmartHome
{
    public class DiscoveryResponsePayload
    {
        [JsonProperty("endpoints")]
        public List<DiscoveryEndpoint> Endpoints { get; set; }
    }
}
