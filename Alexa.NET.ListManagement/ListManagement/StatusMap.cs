using Newtonsoft.Json;

namespace Alexa.NET.ListManagement
{
    internal class StatusMap
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}