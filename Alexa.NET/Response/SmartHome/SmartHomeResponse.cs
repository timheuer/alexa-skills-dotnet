using Newtonsoft.Json;

namespace Alexa.NET.Response.SmartHome
{
    public class SmartHomeResponse
    {
        [JsonProperty("event")]
        public SmartHomeEvent Event { get; set; }

        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public SmartHomeContext Context { get; set; }
    }
}
