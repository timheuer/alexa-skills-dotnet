using Newtonsoft.Json;

namespace Alexa.NET.Request
{
    public class RequestExperimentation
    {
        [JsonProperty("activeExperiments",NullValueHandling = NullValueHandling.Ignore)]
        public Experiment[] ActiveExperiments { get; set; }
    }
}