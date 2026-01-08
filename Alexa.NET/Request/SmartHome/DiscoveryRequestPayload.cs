using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alexa.NET.Request.SmartHome
{
    public class DiscoveryRequestPayload
    {
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public Scope Scope { get; set; }
    }
}
