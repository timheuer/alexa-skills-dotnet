using Newtonsoft.Json;

namespace Alexa.NET.Response.SmartHome
{
    public class CapabilityPropertyName
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
