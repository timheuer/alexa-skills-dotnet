using Newtonsoft.Json;

namespace Alexa.NET
{
    public class Resolution
    {
        [JsonProperty("resolutionsPerAuthority")]
        public ResolutionAuthority[] Authorities { get; set; }
    }
}
