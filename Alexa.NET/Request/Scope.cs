using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Scope
    {
        [JsonProperty("status")]
        public string status { get; set; }
    }
}
