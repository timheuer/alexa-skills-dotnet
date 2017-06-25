using Newtonsoft.Json;

namespace Alexa.NET
{
    public class ResolutionAuthority
    {
        [JsonProperty("authority")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public ResolutionStatus Status { get; set; }

        [JsonProperty("values")]
        public ResolutionValueContainer[] Values { get; set; }
    }
}
