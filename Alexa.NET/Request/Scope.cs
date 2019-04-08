using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Scope
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
