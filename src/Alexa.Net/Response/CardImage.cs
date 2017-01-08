using Newtonsoft.Json;

namespace Alexa.NET.Response
{
    public class CardImage
    {
        [JsonProperty("smallImageUrl")]
        [JsonRequired]
        public string SmallImageUrl { get; set; }

        [JsonProperty("largeImageUrl")]
        [JsonRequired]
        public string LargeImageUrl { get; set; }
    }
}
