using Newtonsoft.Json;

namespace Alexa.NET.ConnectionTasks.Inputs
{
    public class STPWebsiteLink : ISendToPhoneFallbackLink
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}