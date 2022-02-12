using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPUniversalLink : ISendToPhoneFallbackLink
    {
        [JsonProperty("appIdentifier")]
        public string AppIdentifier { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}