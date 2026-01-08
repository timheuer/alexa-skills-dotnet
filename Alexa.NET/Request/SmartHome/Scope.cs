using Newtonsoft.Json;

namespace Alexa.NET.Request.SmartHome
{
    public class Scope
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("partition", NullValueHandling = NullValueHandling.Ignore)]
        public string Partition { get; set; }

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
    }
}
