using Newtonsoft.Json;

namespace Alexa.NET
{
    public class ResolutionValueContainer
    {
        [JsonProperty("value")]
        public ResolutionValue Value { get; set; }
    }
}
