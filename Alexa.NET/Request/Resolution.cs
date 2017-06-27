using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class Resolution
    {
        [JsonProperty("resolutionsPerAuthority")]
        public ResolutionAuthority[] Authorities { get; set; }
    }
}
