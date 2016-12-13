using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class SimpleCard : ICard
    {
        [JsonProperty("type")]
        [JsonRequired]
        public string Type
        {
            get { return "Simple"; }
        }

        [JsonProperty("title")]
        [JsonRequired]
        public string Title { get; set; }

        [JsonRequired]
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}