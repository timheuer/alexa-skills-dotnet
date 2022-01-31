using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class ExperimentVariant
    {
        [JsonProperty("name",NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}