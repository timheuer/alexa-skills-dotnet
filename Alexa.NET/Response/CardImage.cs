using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class CardImage
    {
        [JsonProperty("smallImageUrl",NullValueHandling = NullValueHandling.Ignore)]
        public string SmallImageUrl { get; set; }

        [JsonProperty("largeImageUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string LargeImageUrl { get; set; }
    }
}
