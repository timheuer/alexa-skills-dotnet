using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class StandardCard : ICard
    {
        [JsonProperty("type")]
        [JsonRequired]
        public string Type
        {
            get { return "Standard"; }
        }

        [JsonRequired]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonRequired]
        [JsonProperty("text")]
        public string Content { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public CardImage Image { get; set; }
    }
}
