using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class AuthenticationConfidenceLevel
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("customPolicy",NullValueHandling = NullValueHandling.Ignore)]
        public AuthenticationConfidenceLevelCustomPolicy Custom { get; set; }
    }
}