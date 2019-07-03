using Newtonsoft.Json;

namespace Alexa.NET.Request.Type
{
    public class LaunchRequest : Request
    {
        [JsonProperty("task",NullValueHandling = NullValueHandling.Ignore)]
        public LaunchRequestTask Task { get; set; }
    }
}