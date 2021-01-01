using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class AuthenticationConfidenceLevel
    {
        [JsonProperty("level")]
        public int Level { get; set; }
    }
}