using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class ResolutionValueContainer
    {
        [JsonProperty("value")]
        public ResolutionValue Value { get; set; }
    }
}
